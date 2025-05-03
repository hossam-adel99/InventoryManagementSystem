using Inventory_Management_System.CQRS.Products.Commands;
using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using MediatR;

namespace Inventory_Management_System.CQRS.Warehouses.Commands
{
    public class AddWarehouseCommand:IRequest<int>
    {
        public AddWarehouseDTO addWarehouseDTO { get; set; }
        public AddWarehouseCommand(AddWarehouseDTO dto)
        {
            addWarehouseDTO = dto;
        }
    }

    public class AddWarehouseCommandHandler : IRequestHandler<AddWarehouseCommand, int>
    {
        private readonly IWarehouseRepo _WarehouseRepo;
        public AddWarehouseCommandHandler(IWarehouseRepo WarehouseRepo)
        {
            _WarehouseRepo = WarehouseRepo;
        }

        public async Task<int> Handle(AddWarehouseCommand request, CancellationToken cancellationToken)
        {
            var dto = request.addWarehouseDTO;

            var newWarehouse = new Models.Warehouse
            {
                Name = dto.Name,
                Location = dto.Location
            };

            await _WarehouseRepo.AddAsync(newWarehouse);
            await _WarehouseRepo.SaveAsync();
            return newWarehouse.Id;
        }
    }
}
