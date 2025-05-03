using Inventory_Management_System.CQRS.Products.Commands;
using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.CQRS.Stocktakings.Commands;
using Inventory_Management_System.CQRS.Stocktakings.Queries;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Products.Orchestrators
{
    public class RemoveProductAndStocktakingOrchestrator:IRequest<bool>
    {
        public int Id { get; set; }
    }


    public class RemoveProductAndStocktakingOrchestratorHandler : IRequestHandler<RemoveProductAndStocktakingOrchestrator, bool>
    {
        private readonly IMediator _mediator;
        public RemoveProductAndStocktakingOrchestratorHandler(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<bool> Handle(RemoveProductAndStocktakingOrchestrator request, CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(new GetProductByIdQuery{ Id = request.Id });
            if (product == null)
            {
                return false;
            }

            var stocktakingList = await _mediator.Send(new GetStocktakingsByProductIdQuery{ productId=request.Id});
            if (stocktakingList != null)
            {
                foreach (var stocktaking in stocktakingList)
                {
                    await _mediator.Send(new RemoveStocktakingCommand{ productId = stocktaking.ProductId,warehouseId = stocktaking.WarehouseId});
                }
            }

            var result = await _mediator.Send(new RemoveProductCommand{ Id = request.Id });

            return result;
        }
    }
}
