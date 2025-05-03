using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Repositories;
using MediatR;

namespace Inventory_Management_System.CQRS.Products.Commands
{
    public class RemoveProductCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }


    public class RemoveProductCommandHandler : IRequestHandler<RemoveProductCommand, bool>
    {
        private readonly IProductRepo _productRepo;
        public RemoveProductCommandHandler(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public async Task<bool> Handle(RemoveProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _productRepo.GetByIdAsync(request.Id);
            if (product == null)
            {
                return false;
            }

            product.IsDeleted = true;
            await _productRepo.UpdateAsync(product);
            await _productRepo.SaveAsync();
            return true;
        }
    }
}