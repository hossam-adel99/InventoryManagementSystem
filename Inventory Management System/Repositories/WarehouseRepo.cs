using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Repositories
{
    public class WarehouseRepo : IWarehouseRepo
    {
        private readonly MyContext myContext;

        public WarehouseRepo(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public async Task AddAsync(Warehouse obj)
        {
            await myContext.Warehouses.AddAsync(obj);
        }

        public async Task<IEnumerable<Warehouse>> GetAllAsync()
        {
            return await myContext.Warehouses.Where(c => c.IsDeleted == false).ToListAsync();
        }

        public async Task<Warehouse> GetByIdAsync(int id)
        {
            return await myContext.Warehouses.Where(c => c.Id == id && c.IsDeleted == false).FirstOrDefaultAsync();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await myContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Warehouse entity)
        {
            myContext.Warehouses.Update(entity);
        }

    }
}