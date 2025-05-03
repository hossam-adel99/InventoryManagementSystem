using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Products.Queries
{
    public class GetAllProductsQuery:IRequest<ICollection<GetProductDTO>>
    {
    }

    public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, ICollection<GetProductDTO>>
    {
        private readonly IProductRepo productRepo;
        public GetAllProductsQueryHandler(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<ICollection<GetProductDTO>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var productFromDb = await productRepo.GetAllAsync();
            if(productFromDb==null)
            {
                return null;
            }
            ICollection<GetProductDTO> products =new List<GetProductDTO>();
            foreach (var product in productFromDb)
            {
                products.Add(new GetProductDTO
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Description = product.Description,
                    Quantity = product.Quantity,
                    AvailableQuantity = product.AvailableQuantity,
                    CategoryId = product.CategoryId,
                    LowStockThreshold = product.LowStockThreshold
                });
            }
            
            return products;
        }
    }
}
