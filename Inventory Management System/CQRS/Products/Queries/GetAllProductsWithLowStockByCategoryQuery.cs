using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Products.Queries
{
    public class GetAllProductsWithLowStockByCategoryQuery : IRequest<ICollection<GetProductDTO>>
    {
        public int categoryId { get; set; }
    }

    public class GetAllProductsWithLowStockByCategoryQueryHandler : IRequestHandler<GetAllProductsWithLowStockByCategoryQuery, ICollection<GetProductDTO>>
    {
        private readonly IProductRepo productRepo;
        public GetAllProductsWithLowStockByCategoryQueryHandler(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

        public async Task<ICollection<GetProductDTO>> Handle(GetAllProductsWithLowStockByCategoryQuery request, CancellationToken cancellationToken)
        {
            var productList = await productRepo.GetAllWithLowStockByCategoryAsync(request.categoryId);
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
