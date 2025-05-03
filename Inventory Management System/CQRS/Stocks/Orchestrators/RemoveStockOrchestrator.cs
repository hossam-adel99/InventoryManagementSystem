using Azure.Core;
using Inventory_Management_System.CQRS.Products.Commands;
using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.CQRS.Stocktakings.Commands;
using Inventory_Management_System.CQRS.Stocktakings.GetProductById;
using Inventory_Management_System.CQRS.Stocktakings.Queries;
using Inventory_Management_System.CQRS.Transactions.Commands;
using Inventory_Management_System.CQRS.Warehouses.Queries;
using Inventory_Management_System.DTO;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Stocks.Orchestrators
{
    public class RemoveStockOrchestrator:IRequest<int>
    {
        public AddStocktakingDTO stocktakingDTO { get; set; }
        public RemoveStockOrchestrator(AddStocktakingDTO dto)
        {
            stocktakingDTO = dto;
        }
    }

    public class RemoveStockOrchestratorHandler : IRequestHandler<RemoveStockOrchestrator, int>
    {
        private readonly IMediator _mediator;
        public RemoveStockOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Handle(RemoveStockOrchestrator request, CancellationToken cancellationToken)
        {
            AddStocktakingDTO stocktakingDTO = request.stocktakingDTO;

            GetProductDTO productFromDb = await _mediator.Send(new GetProductByIdQuery { Id = stocktakingDTO.ProductId });
            if (productFromDb == null)
            {
                return -1;
            }

            Warehouse warehouseFromDb = await _mediator.Send(new GetWarehouseByIdQuery{ Id = stocktakingDTO.WarehouseId });
            if (warehouseFromDb == null)
            {
                return -2;
            }

            int oldStockQuantity = 0;
            Stocktaking stocktakingFromDb = await _mediator.Send(new GetStocktakingByCompositeIdQuery { productId = stocktakingDTO.ProductId, warehouseId = stocktakingDTO.WarehouseId });
            if (stocktakingFromDb == null)
            {
                return -3;
            }

            oldStockQuantity = stocktakingDTO.Stock;
            int newStock = stocktakingFromDb.Stock - oldStockQuantity;

            if (newStock < 0)
            {
                return -4;
            }
            else if (newStock == 0)
            {
                await _mediator.Send(new RemoveStocktakingCommand{productId = stocktakingDTO.ProductId,warehouseId = stocktakingDTO.WarehouseId});
            }
            else
            {
                stocktakingDTO.Stock = newStock;
                await _mediator.Send(new EditStockingCommand(stocktakingDTO));
            }


            productFromDb.Quantity -= oldStockQuantity;
            AddProductDTO UpdatedProduct = new AddProductDTO
            {
                Id=stocktakingDTO.ProductId,
                Name = productFromDb.Name,
                Price = productFromDb.Price,
                Description = productFromDb.Description,
                Quantity = productFromDb.Quantity,
                CategoryId = productFromDb.CategoryId,
                LowStockThreshold = productFromDb.LowStockThreshold
            };
            int UpdatedProductId = await _mediator.Send(new EditProductForStockCommand(UpdatedProduct));

            AddTransactionDTO newTransaction = new AddTransactionDTO
            {
                Type = TransactionType.Remove,
                QuantityChanged = oldStockQuantity,
                ProductId = stocktakingDTO.ProductId,
                WarehouseId = stocktakingDTO.WarehouseId,
            };
            int newTransactionId = await _mediator.Send(new AddTransactionCommand(newTransaction));

            return newTransactionId;
        }
    }
}
