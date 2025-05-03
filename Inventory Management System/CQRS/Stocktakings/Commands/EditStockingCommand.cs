using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Stocktakings.GetProductById
{
    public class EditStockingCommand : IRequest<int>
    {

        public AddStocktakingDTO StocktakingDTO { get; set; }
        public EditStockingCommand(AddStocktakingDTO dto)
        {
            StocktakingDTO = dto;
        }
    }

    public class EditStockingCommandHandler : IRequestHandler<EditStockingCommand, int>
    {
        private readonly IStocktakingRepo _stocktakingRepo;
        public EditStockingCommandHandler(IStocktakingRepo stocktakingRepo)
        {
            _stocktakingRepo = stocktakingRepo;
        }

        public async Task<int> Handle(EditStockingCommand request, CancellationToken cancellationToken)
        {
            var StockFromRequest = request.StocktakingDTO;

            var stocktaking = await _stocktakingRepo.GetByCompositeIdIgnoreIsDeletedAsync(StockFromRequest.ProductId, StockFromRequest.WarehouseId);
            if (stocktaking == null)
            {
                return 0;
            }

            stocktaking.Stock = StockFromRequest.Stock;
            stocktaking.IsDeleted = StockFromRequest.IsDeleted;

            await _stocktakingRepo.UpdateAsync(stocktaking);
            await _stocktakingRepo.SaveAsync();
            return stocktaking.ProductId;
        }
    }
}
