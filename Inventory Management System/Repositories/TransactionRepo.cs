using Azure.Core;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Inventory_Management_System.Repositories
{
    public class TransactionRepo : ITransactionRepo
    {
        private readonly MyContext myContext;
        public TransactionRepo(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public async Task AddAsync(InventoryTransaction obj)
        {
            await myContext.InventoryTransactions.AddAsync(obj);
        }

        public async Task<IEnumerable<InventoryTransaction>> GetAllAsync()
        {
            return await myContext.InventoryTransactions.ToListAsync();
        }
        public async Task<IEnumerable<InventoryTransaction>> GetAllByProductIdAsync(int productId)
        {
            return await myContext.InventoryTransactions.Where(t => t.ProductId == productId).ToListAsync();
        }

        public async Task<InventoryTransaction> GetByIdAsync(int id)
        {
            return await myContext.InventoryTransactions.FindAsync(id);
        }

        public async Task RemoveAsync(int id)
        {
            InventoryTransaction obj = await GetByIdAsync(id);
            myContext.InventoryTransactions.Remove(obj); 
        }

        public async Task SaveAsync()
        {
            await myContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(InventoryTransaction obj)
        {
            myContext.InventoryTransactions.Update(obj);
        }
    }
}
