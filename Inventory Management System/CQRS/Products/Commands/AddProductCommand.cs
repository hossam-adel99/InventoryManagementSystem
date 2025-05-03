using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Inventory_Management_System.CQRS.Products.Commands
{
    public class AddProductCommand : IRequest<int>
    {
        public AddProductDTO AddProductDTO { get; set; }
        public AddProductCommand(AddProductDTO dto)
        {
            AddProductDTO = dto;
        }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, int>
    {
        private readonly IProductRepo _productRepo;
        public AddProductCommandHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<int> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var dto = request.AddProductDTO;

            var newProduct = new Models.Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                Quantity = dto.Quantity,
                AvailableQuantity = dto.Quantity,
                CategoryId = dto.CategoryId,
                LowStockThreshold = dto.LowStockThreshold
            };

            await _productRepo.AddAsync(newProduct);
            await _productRepo.SaveAsync();
            return newProduct.Id;
        }
    }
}
