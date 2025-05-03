using Inventory_Management_System.Models;
using Quiz.Interface;

namespace Inventory_Management_System.Interfaces
{
    public interface ITransactionRepo:IGenericRepository<InventoryTransaction>
    {
        public Task<IEnumerable<InventoryTransaction>> GetAllByProductIdAsync(int productId);
    }
}
