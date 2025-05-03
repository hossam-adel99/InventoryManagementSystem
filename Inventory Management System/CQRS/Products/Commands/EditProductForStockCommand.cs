using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Products.Commands
{
    public class EditProductForStockCommand:IRequest<int>
    {
        public AddProductDTO AddProductDTO { get; set; }
        public EditProductForStockCommand(AddProductDTO dto)
        {
            AddProductDTO = dto;
        }
    }

    public class EditProductForStockCommandHandler : IRequestHandler<EditProductForStockCommand, int>
    {
        private readonly IProductRepo productRepo;
        public EditProductForStockCommandHandler(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<int> Handle(EditProductForStockCommand request, CancellationToken cancellationToken)
        {
            var ProductFromRequest = request.AddProductDTO;

            var product = await productRepo.GetByIdAsync(ProductFromRequest.Id);
            if (product == null)
            {
                return 0;
            }

            product.Name = ProductFromRequest.Name;
            product.Price = ProductFromRequest.Price;
            product.Description = ProductFromRequest.Description;
            product.Quantity = ProductFromRequest.Quantity;
            product.AvailableQuantity = ProductFromRequest.AvailableQuantity;
            product.CategoryId = ProductFromRequest.CategoryId;
            product.LowStockThreshold = ProductFromRequest.LowStockThreshold;

            await productRepo.UpdateAsync(product);
            await productRepo.SaveAsync();
            return product.Id;
        }
    }
}

