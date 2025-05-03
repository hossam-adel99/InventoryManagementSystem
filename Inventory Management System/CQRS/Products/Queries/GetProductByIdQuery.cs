using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Products.Queries
{
    public class GetProductByIdQuery : IRequest<GetProductDTO>
    {
        public int Id { get; set; }
    }

    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductDTO>
    {
        private readonly IProductRepo productRepo;
        public GetProductByIdQueryHandler(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<GetProductDTO?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var productFromDb = await productRepo.GetByIdAsync(request.Id);
            if(productFromDb==null)
            {
                return null;
            }
            GetProductDTO product = new GetProductDTO
            {
                Id = productFromDb.Id,
                Name = productFromDb.Name,
                Price = productFromDb.Price,
                Description = productFromDb.Description,
                Quantity = productFromDb.Quantity,
                AvailableQuantity = productFromDb.AvailableQuantity,
                CategoryId = productFromDb.CategoryId,
                LowStockThreshold = productFromDb.LowStockThreshold
            };
            return product;
        }
    }
}
