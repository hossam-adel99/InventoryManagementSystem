using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Stocktakings.Queries
{
    public class GetStocktakingsByWarehouseIdQuery : IRequest<ICollection<Stocktaking>>
    {
        public int warehouseId { get; set; }
    }

    public class GetStocktakingsByWarehouseIdQueryHandler : IRequestHandler<GetStocktakingsByWarehouseIdQuery, ICollection<Stocktaking>>
    {
        private readonly IStocktakingRepo _stocktakingRepo;
        public GetStocktakingsByWarehouseIdQueryHandler(IStocktakingRepo stocktakingRepo)
        {
            _stocktakingRepo = stocktakingRepo;
        }

        public async Task<ICollection<Stocktaking>> Handle(GetStocktakingsByWarehouseIdQuery request, CancellationToken cancellationToken)
        {
            ICollection<Stocktaking> StocktakingList = (ICollection<Stocktaking>)await _stocktakingRepo.GetStocktakingsByWarehouseId(request.warehouseId);

            return StocktakingList;
        }
    }
}


