using Inventory_Management_System.CQRS.Products.Commands;
using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Warehouses.Commands
{
    public class EditWarehouseCommand:IRequest<int>
    {
        public AddWarehouseDTO AddWarehouseDTO { get; set; }
        public EditWarehouseCommand(AddWarehouseDTO dto)
        {
            AddWarehouseDTO = dto;
        }
    }

    public class EditWarehouseCommandHandler : IRequestHandler<EditWarehouseCommand, int>
    {
        private readonly IWarehouseRepo _warehouseRepo;
        public EditWarehouseCommandHandler(IWarehouseRepo warehouseRepo)
        {
            _warehouseRepo = warehouseRepo;
        }

        public async Task<int> Handle(EditWarehouseCommand request, CancellationToken cancellationToken)
        {
            var WarehouseFromRequest = request.AddWarehouseDTO;

            var warehouse = await _warehouseRepo.GetByIdAsync(WarehouseFromRequest.Id);
            if (warehouse == null)
            {
                return 0;
            }
            warehouse.Id = WarehouseFromRequest.Id;
            warehouse.Name = WarehouseFromRequest.Name;
            warehouse.Location = WarehouseFromRequest.Location;

            await _warehouseRepo.UpdateAsync(warehouse);
            await _warehouseRepo.SaveAsync();
            return warehouse.Id;
        }
    }
}
