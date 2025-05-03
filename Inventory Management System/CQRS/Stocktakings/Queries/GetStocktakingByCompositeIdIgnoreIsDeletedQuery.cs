using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Stocktakings.Queries
{
    public class GetStocktakingByCompositeIdIgnoreIsDeletedQuery : IRequest<Stocktaking>
    {
        public int productId { get; set; }
        public int warehouseId { get; set; }
    }

    public class GetStocktakingByCompositeIdIgnoreIsDeletedQueryHandler : IRequestHandler<GetStocktakingByCompositeIdIgnoreIsDeletedQuery, Stocktaking>
    {
        private readonly IStocktakingRepo _stocktakingRepo;
        public GetStocktakingByCompositeIdIgnoreIsDeletedQueryHandler(IStocktakingRepo stocktakingRepo)
        {
            _stocktakingRepo = stocktakingRepo;
        }

        public async Task<Stocktaking?> Handle(GetStocktakingByCompositeIdIgnoreIsDeletedQuery request, CancellationToken cancellationToken)
        {
            Stocktaking stocktaking = await _stocktakingRepo.GetByCompositeIdIgnoreIsDeletedAsync(request.productId, request.warehouseId);

            return stocktaking;
        }
    }
}

