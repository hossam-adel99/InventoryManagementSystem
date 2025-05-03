using Inventory_Management_System.Models;
using Quiz.Interface;

namespace Inventory_Management_System.Interfaces
{
    public interface IProductRepo:IGenericRepository<Product>
    {
        public Task<IEnumerable<Product>> GetAllWithLowStockAsync();
        public Task<IEnumerable<Product>> GetAllWithLowStockByCategoryAsync(int CaegorytId);
    }
}
