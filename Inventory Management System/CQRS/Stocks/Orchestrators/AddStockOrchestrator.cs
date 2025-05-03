using Inventory_Management_System.CQRS.Products.Commands;
using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.CQRS.Stocktakings.Commands;
using Inventory_Management_System.CQRS.Stocktakings.GetProductById;
using Inventory_Management_System.CQRS.Stocktakings.Queries;
using Inventory_Management_System.CQRS.Transactions.Commands;
using Inventory_Management_System.CQRS.Warehouses.Queries;
using Inventory_Management_System.DTO;
using Inventory_Management_System.Enums;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Stocks.Orchestrators
{
    public class AddStockOrchestrator:IRequest<int>
    {
        public AddStocktakingDTO stocktakingDTO { get; set; }
        public AddStockOrchestrator(AddStocktakingDTO dto)
        {
            stocktakingDTO = dto;
        }
    }

    public class AddStockOrchestratorHandler : IRequestHandler<AddStockOrchestrator, int>
    {
        private readonly IMediator _mediator;
        public AddStockOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Handle(AddStockOrchestrator request, CancellationToken cancellationToken)
        {
            AddStocktakingDTO stocktakingDTO = request.stocktakingDTO;
            int oldStockQuantity = stocktakingDTO.Stock;
            GetProductDTO productFromDb = await _mediator.Send(new GetProductByIdQuery { Id = stocktakingDTO.ProductId });
            if (productFromDb == null)
            {
                return -1;
            }
            else if(stocktakingDTO.Stock> productFromDb.AvailableQuantity)
            {
                return -1;
            }

             Warehouse warehouseFromDb = await _mediator.Send(new GetWarehouseByIdQuery { Id = stocktakingDTO.WarehouseId });
            if (warehouseFromDb == null)
            {
                return -2;
            }

            Stocktaking stocktakingFromDb = await _mediator.Send(new GetStocktakingByCompositeIdIgnoreIsDeletedQuery { productId = stocktakingDTO.ProductId, warehouseId = stocktakingDTO.WarehouseId });
            if (stocktakingFromDb == null)
            {
                int newStockId = await _mediator.Send(new AddStocktakingCommand(stocktakingDTO));
            }
            else
            {
                stocktakingDTO.Stock += stocktakingFromDb.Stock;
                stocktakingDTO.IsDeleted = false;
                int UpdatedStockId = await _mediator.Send(new EditStockingCommand(stocktakingDTO));
            }

            AddProductDTO UpdatedProduct = new AddProductDTO
            {
                Id = productFromDb.Id,
                Name = productFromDb.Name,
                Price = productFromDb.Price,
                Description = productFromDb.Description,
                Quantity = productFromDb.Quantity,
                AvailableQuantity=productFromDb.AvailableQuantity - oldStockQuantity,
                CategoryId = productFromDb.CategoryId,
                LowStockThreshold = productFromDb.LowStockThreshold
            };
            int UpdatedProductId = await _mediator.Send(new EditProductForStockCommand(UpdatedProduct));

            AddTransactionDTO newTransaction = new AddTransactionDTO
            {
                Type = TransactionType.Add,
                QuantityChanged = oldStockQuantity,
                ProductId=stocktakingDTO.ProductId,
                WarehouseId=stocktakingDTO.WarehouseId,
            };
            int newTransactionId = await _mediator.Send(new AddTransactionCommand(newTransaction));

            return newTransactionId;
        }
    }
}
