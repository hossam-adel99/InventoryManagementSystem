using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Stocktakings.Commands
{
    public class RemoveStocktakingCommand : IRequest<bool>
    {
        public int productId { get; set; }
        public int warehouseId { get; set; }
    }


    public class RemoveStocktakingCommandHandler : IRequestHandler<RemoveStocktakingCommand, bool>
    {
        private readonly IStocktakingRepo _stocktakingRepo;

        public RemoveStocktakingCommandHandler(IStocktakingRepo stocktakingRepo)
        {
            _stocktakingRepo = stocktakingRepo;
        }

        public async Task<bool> Handle(RemoveStocktakingCommand request, CancellationToken cancellationToken)
        {
            var stocktaking = await _stocktakingRepo.GetByCompositeIdAsync(request.productId,request.warehouseId);
            if (stocktaking == null)
            {
                return false;
            }

            stocktaking.IsDeleted = true;
            stocktaking.Stock = 0;
            await _stocktakingRepo.UpdateAsync(stocktaking);
            await _stocktakingRepo.SaveAsync();
            return true;
        }
    }
}