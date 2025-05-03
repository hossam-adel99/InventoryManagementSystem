using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Stocktakings.Queries
{
    public class GetStocktakingsByProductIdQuery: IRequest<ICollection<Stocktaking>>
    {
        public int productId { get; set; }
    }

    public class GetStocktakingsByProductIdQueryHandler : IRequestHandler<GetStocktakingsByProductIdQuery, ICollection<Stocktaking>>
    {
        private readonly IStocktakingRepo _stocktakingRepo;
        public GetStocktakingsByProductIdQueryHandler(IStocktakingRepo stocktakingRepo)
        {
            _stocktakingRepo = stocktakingRepo;
        }

        public async Task<ICollection<Stocktaking>> Handle(GetStocktakingsByProductIdQuery request, CancellationToken cancellationToken)
        {
            ICollection<Stocktaking> StocktakingList = (ICollection<Stocktaking>)await _stocktakingRepo.GetStocktakingsByProductId(request.productId);

            return StocktakingList;
        }
    }
}

