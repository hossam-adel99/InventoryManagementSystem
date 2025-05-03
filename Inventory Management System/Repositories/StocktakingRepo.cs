using System.Collections.ObjectModel;
using Azure.Core;
using System.Threading;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Repositories
{
    public class StocktakingRepo : IStocktakingRepo
    {
        private readonly MyContext myContext;
        public StocktakingRepo(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public async Task AddAsync(Stocktaking obj)
        {
            await myContext.Stocktakings.AddAsync(obj);
        }

        public async Task<IEnumerable<Stocktaking>> GetAllAsync()
        {
            return await myContext.Stocktakings.Where(c => c.IsDeleted == false).ToListAsync();
        }

        public async Task<Stocktaking> GetByCompositeIdAsync(int productId,int warehouseId)
        {
            return await myContext.Stocktakings.Where(s=>s.ProductId==productId&&s.WarehouseId==warehouseId && s.IsDeleted==false).FirstOrDefaultAsync();
        }
        public async Task<Stocktaking> GetByCompositeIdIgnoreIsDeletedAsync(int productId, int warehouseId)
        {
            return await myContext.Stocktakings.Where(s => s.ProductId == productId && s.WarehouseId == warehouseId).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Stocktaking>> GetStocktakingsByProductId(int productId)
        {
            return await myContext.Stocktakings.Where(s => s.ProductId == productId && s.IsDeleted==false).ToListAsync();
        }

        public async Task<IEnumerable<Stocktaking>> GetStocktakingsByWarehouseId(int warehouseId)
        {
            return await myContext.Stocktakings.Where(s => s.WarehouseId == warehouseId && s.IsDeleted == false).ToListAsync();
        }
        public Task<Stocktaking> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        //public async Task RemoveAsync(int productId, int warehouseId)
        //{
        //    Stocktaking obj = await GetByCompositeIdAsync( productId,  warehouseId);
        //    myContext.Stocktakings.Remove(obj);
        //}

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await myContext.SaveChangesAsync();
        }


        public async Task UpdateAsync(Stocktaking obj)
        {
            myContext.Stocktakings.Update(obj);
        }
    }
}
