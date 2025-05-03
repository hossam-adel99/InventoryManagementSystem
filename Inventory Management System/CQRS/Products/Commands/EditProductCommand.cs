using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Products.Commands
{
    public class EditProductCommand:IRequest<int>
    {
        public AddProductDTO AddProductDTO { get; set; }
        public EditProductCommand(AddProductDTO dto)
        {
            AddProductDTO = dto;
        }
    }

    public class EditProductCommandHandler : IRequestHandler<EditProductCommand, int>
    {
        private readonly IProductRepo productRepo;
        public EditProductCommandHandler(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<int> Handle(EditProductCommand request, CancellationToken cancellationToken)
        {
            var ProductFromRequest = request.AddProductDTO;

            var product =await productRepo.GetByIdAsync(ProductFromRequest.Id);   
            if (product == null)
            {
                return 0;
            }
            int oldQuantity= product.Quantity;
            int QuantityAdded = ProductFromRequest.Quantity - oldQuantity;
            int newAvailableQuantity = product.AvailableQuantity + QuantityAdded;

            product.Name = ProductFromRequest.Name;
            product.Price = ProductFromRequest.Price;
            product.Description = ProductFromRequest.Description;
            product.Quantity = ProductFromRequest.Quantity;
            product.AvailableQuantity = newAvailableQuantity;
            product.CategoryId = ProductFromRequest.CategoryId;
            product.LowStockThreshold = ProductFromRequest.LowStockThreshold;

            await productRepo.UpdateAsync(product);
            await productRepo.SaveAsync();
            return product.Id;
        }
    }
}
