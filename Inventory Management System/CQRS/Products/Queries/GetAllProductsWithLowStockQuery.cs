using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Products.Queries
{
    public class GetAllProductsWithLowStockQuery : IRequest<ICollection<GetProductDTO>>
    {
        
    }

    public class GetAllProductsWithLowStockQueryHandler : IRequestHandler<GetAllProductsWithLowStockQuery, ICollection<GetProductDTO>>
    {
        private readonly IProductRepo productRepo;
        public GetAllProductsWithLowStockQueryHandler(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<ICollection<GetProductDTO>> Handle(GetAllProductsWithLowStockQuery request, CancellationToken cancellationToken)
        {
            var productList = await productRepo.GetAllWithLowStockAsync();
            if (productList == null)
            {
                return null;
            }
            ICollection<GetProductDTO> products = new List<GetProductDTO>();
            foreach (var product in productList)
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

