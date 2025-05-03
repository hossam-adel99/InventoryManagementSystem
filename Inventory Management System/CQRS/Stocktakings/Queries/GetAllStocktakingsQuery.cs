using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace Inventory_Management_System.CQRS.Stocktakings.Queries
{
    public class GetAllStocktakingsQuery:IRequest<ICollection<Stocktaking>>
    {
    }

    public class GetAllStocktakingsQueryHandler : IRequestHandler<GetAllStocktakingsQuery, ICollection<Stocktaking>>
    {
        private readonly IStocktakingRepo _stocktakingRepo;
        public GetAllStocktakingsQueryHandler(IStocktakingRepo stocktakingRepo)
        {
            _stocktakingRepo = stocktakingRepo;
        }

        public async Task<ICollection<Stocktaking>> Handle(GetAllStocktakingsQuery request, CancellationToken cancellationToken)
        {
            ICollection<Stocktaking> StocktakingList = (ICollection<Stocktaking>)await _stocktakingRepo.GetAllAsync();
            
            return StocktakingList;
        }
    }
}
