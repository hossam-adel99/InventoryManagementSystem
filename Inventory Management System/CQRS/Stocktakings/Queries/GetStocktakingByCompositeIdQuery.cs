using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using Inventory_Management_System.Repositories;
using MediatR;

namespace Inventory_Management_System.CQRS.Stocktakings.Queries
{
    public class GetStocktakingByCompositeIdQuery : IRequest<Stocktaking>
    {
        public int productId { get; set; }
        public int warehouseId { get; set; }
    }

    public class GetStocktakingByCompositeIdQueryHandler : IRequestHandler<GetStocktakingByCompositeIdQuery, Stocktaking>
    {
        private readonly IStocktakingRepo _stocktakingRepo;
        public GetStocktakingByCompositeIdQueryHandler(IStocktakingRepo stocktakingRepo)
        {
            _stocktakingRepo = stocktakingRepo;
        }

        public async Task<Stocktaking?> Handle(GetStocktakingByCompositeIdQuery request, CancellationToken cancellationToken)
        {
            Stocktaking stocktaking = await _stocktakingRepo.GetByCompositeIdAsync(request.productId,request.warehouseId);

            return stocktaking;
        }
    }
}
