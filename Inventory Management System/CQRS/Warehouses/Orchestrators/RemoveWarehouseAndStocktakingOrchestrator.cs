using Inventory_Management_System.CQRS.Products.Commands;
using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.CQRS.Stocktakings.Commands;
using Inventory_Management_System.CQRS.Stocktakings.Queries;
using Inventory_Management_System.CQRS.Warehouses.Commands;
using Inventory_Management_System.CQRS.Warehouses.Queries;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Products.Orchestrators
{
    public class RemoveWarehouseAndStocktakingOrchestrator:IRequest<bool>
    {
        public int Id { get; set; }
    }


    public class RemoveWarehouseAndStocktakingOrchestratorHandler : IRequestHandler<RemoveWarehouseAndStocktakingOrchestrator, bool>
    {
        private readonly IMediator _mediator;
        public RemoveWarehouseAndStocktakingOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(RemoveWarehouseAndStocktakingOrchestrator request, CancellationToken cancellationToken)
        {
            var warehouse = await _mediator.Send(new GetWarehouseByIdQuery{ Id = request.Id });
            if (warehouse == null)
            {
                return false;
            }

            var stocktakingList = await _mediator.Send(new GetStocktakingsByWarehouseIdQuery{ warehouseId=request.Id});
            if (stocktakingList != null)
            {
                foreach (var stocktaking in stocktakingList)
                {
                    await _mediator.Send(new RemoveStocktakingCommand{ productId = stocktaking.ProductId,warehouseId = stocktaking.WarehouseId});
                }
            }

            var result = await _mediator.Send(new RemoveWarehouseCommand{ Id = request.Id });

            return result;
        }
    }
}
