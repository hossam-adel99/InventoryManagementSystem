using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Inventory_Management_System.CQRS.Stocktakings.Commands
{
    public class AddStocktakingCommand : IRequest<int>
    {
        public AddStocktakingDTO stocktakingDTO { get; set; }
        public AddStocktakingCommand(AddStocktakingDTO dto)
        {
            stocktakingDTO = dto;        }
    }

    public class AddStocktakingCommandHandler : IRequestHandler<AddStocktakingCommand, int>
    {
        private readonly IStocktakingRepo _stocktakingRepo;
        public AddStocktakingCommandHandler(IStocktakingRepo stockRepo)
        {
            _stocktakingRepo = stockRepo;
        }

        public async Task<int> Handle(AddStocktakingCommand request, CancellationToken cancellationToken)
        {
            var dto = request.stocktakingDTO;
            Stocktaking stocktakingFromDb = await _stocktakingRepo.GetByCompositeIdIgnoreIsDeletedAsync(dto.ProductId, dto.WarehouseId);
            if(stocktakingFromDb !=null)
            {
                return 0;
            }

            var newStocktaking = new Stocktaking
            {
                Stock=dto.Stock,
                ProductId=dto.ProductId,
                WarehouseId=dto.WarehouseId
            };

            await _stocktakingRepo.AddAsync(newStocktaking);
            await _stocktakingRepo.SaveAsync();
            return newStocktaking.ProductId;
        }
    }
}
