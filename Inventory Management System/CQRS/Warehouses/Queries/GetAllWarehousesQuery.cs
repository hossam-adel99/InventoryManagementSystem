using Inventory_Management_System.CQRS.Products.Queries;
using Inventory_Management_System.DTO;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using MediatR;

namespace Inventory_Management_System.CQRS.Warehouses.Queries
{
    public class GetAllWarehousesQuery:IRequest<ICollection<Warehouse>>
    {
    }

    public class GetAllWarehousesQueryHandler : IRequestHandler<GetAllWarehousesQuery, ICollection<Warehouse>>
    {
        private readonly IWarehouseRepo _warehouseRepo;
        public GetAllWarehousesQueryHandler(IWarehouseRepo warehouseRepo)
        {
            _warehouseRepo = warehouseRepo;
        }

        public async Task<ICollection<Warehouse>> Handle(GetAllWarehousesQuery request, CancellationToken cancellationToken)
        {
            ICollection<Warehouse> WarehousesFromDb = (ICollection<Warehouse>)await _warehouseRepo.GetAllAsync();

            return WarehousesFromDb;
        }
    }
}

