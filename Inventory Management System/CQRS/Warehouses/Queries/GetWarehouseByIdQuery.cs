using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Warehouses.Queries
{
    public class GetWarehouseByIdQuery:IRequest<Warehouse>
    {
        public int Id { get; set; }
    }

    public class GetWarehouseByIdQueryHandler : IRequestHandler<GetWarehouseByIdQuery, Warehouse>
    {
        private readonly IWarehouseRepo _warehouseRepo;
        public GetWarehouseByIdQueryHandler(IWarehouseRepo warehouseRepo)
        {
            _warehouseRepo = warehouseRepo;
        }

        public async Task<Warehouse?> Handle(GetWarehouseByIdQuery request, CancellationToken cancellationToken)
        {
            Warehouse WarehouseFromDb = await _warehouseRepo.GetByIdAsync(request.Id);

            return WarehouseFromDb;
        }
    }
}

