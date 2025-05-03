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
    public class TransferStockOrchestrator:IRequest<int>
    {
        public TransfarStockDTO stocktakingDTO { get; set; }
        public TransferStockOrchestrator(TransfarStockDTO dto)
        {
            stocktakingDTO = dto;
        }
    }

    public class TransferStockOrchestratorHandler : IRequestHandler<TransferStockOrchestrator, int>
    {
        private readonly IMediator _mediator;
        public TransferStockOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<int> Handle(TransferStockOrchestrator request, CancellationToken cancellationToken)
        {
            TransfarStockDTO stockFromReq = request.stocktakingDTO;

            GetProductDTO productFromDb = await _mediator.Send(new GetProductByIdQuery { Id = stockFromReq.ProductId });
            if(productFromDb == null)
            {
                return -1;
            }
            AddProductDTO UpdatedProduct = new AddProductDTO
            {
                Id = productFromDb.Id,
                Name = productFromDb.Name,
                Price = productFromDb.Price,
                Description = productFromDb.Description,
                Quantity = productFromDb.Quantity,
                AvailableQuantity = productFromDb.AvailableQuantity,
                CategoryId = productFromDb.CategoryId,
                LowStockThreshold = productFromDb.LowStockThreshold
            };


            AddStocktakingDTO SourceStocktaking = new AddStocktakingDTO
            {
                Stock = stockFromReq.Stock,
                ProductId = stockFromReq.ProductId,
                WarehouseId = stockFromReq.FromWarehouseId
            };

            AddStocktakingDTO DestinationStocktaking = new AddStocktakingDTO
            {
                Stock = stockFromReq.Stock,
                ProductId = stockFromReq.ProductId,
                WarehouseId = stockFromReq.ToWarehouseId
            };


            AddStocktakingDTO stocktakingDTO = SourceStocktaking;

            Warehouse warehouseFromDb = await _mediator.Send(new GetWarehouseByIdQuery { Id = stocktakingDTO.WarehouseId });
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
                await _mediator.Send(new RemoveStocktakingCommand { productId = stocktakingDTO.ProductId, warehouseId = stocktakingDTO.WarehouseId });
            }
            else
            {
                stocktakingDTO.Stock = newStock;
                await _mediator.Send(new EditStockingCommand(stocktakingDTO));
            }






            int DestinationStocktakingId = await _mediator.Send(new AddStockOrchestrator(DestinationStocktaking));
            if (DestinationStocktakingId <= 0)
            {
                await _mediator.Send(new AddStockOrchestrator(SourceStocktaking));
                UpdatedProduct.Quantity += stockFromReq.Stock;
                UpdatedProduct.AvailableQuantity += stockFromReq.Stock;
                await _mediator.Send(new EditProductForStockCommand(UpdatedProduct));
                return -1;
            }


            UpdatedProduct.Quantity += stockFromReq.Stock;
            UpdatedProduct.AvailableQuantity += stockFromReq.Stock;
            await _mediator.Send(new EditProductForStockCommand(UpdatedProduct));

            AddTransactionDTO newTransaction = new AddTransactionDTO
            {
                Type = TransactionType.Transfer,
                QuantityChanged = stockFromReq.Stock,
                ProductId = stockFromReq.ProductId,
                WarehouseId = stockFromReq.FromWarehouseId,
                FromWarehouseId= stockFromReq.FromWarehouseId,
                ToWarehouseId=stockFromReq.ToWarehouseId
            };
            int newTransactionId = await _mediator.Send(new AddTransactionCommand(newTransaction));

            return newTransactionId;
        }
    }
}
