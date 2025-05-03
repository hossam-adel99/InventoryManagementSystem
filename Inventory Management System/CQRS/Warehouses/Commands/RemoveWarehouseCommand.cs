using Inventory_Management_System.CQRS.Products.Commands;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Warehouses.Commands
{
    public class RemoveWarehouseCommand:IRequest<bool>
    {
        public int Id { get; set; }
    }


    public class RemoveWarehouseCommandHandler : IRequestHandler<RemoveWarehouseCommand, bool>
    {
        private readonly IWarehouseRepo _warehouseRepo;
        public RemoveWarehouseCommandHandler(IWarehouseRepo warehouseRepo)
        {
            _warehouseRepo = warehouseRepo;
        }

        public async Task<bool> Handle(RemoveWarehouseCommand request, CancellationToken cancellationToken)
        {
            var warehouse = await _warehouseRepo.GetByIdAsync(request.Id);
            if (warehouse == null)
            {
                return false;
            }

            warehouse.IsDeleted = true;
            await _warehouseRepo.UpdateAsync(warehouse);
            await _warehouseRepo.SaveAsync();
            return true;
        }
    }
}