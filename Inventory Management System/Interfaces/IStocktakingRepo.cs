using Inventory_Management_System.Models;
using Quiz.Interface;

namespace Inventory_Management_System.Interfaces
{
    public interface IStocktakingRepo:IGenericRepository<Stocktaking>
    {
        public Task<Stocktaking> GetByCompositeIdAsync(int productId, int warehouseId);
        public Task<Stocktaking> GetByCompositeIdIgnoreIsDeletedAsync(int productId, int warehouseId);
        public Task<IEnumerable<Stocktaking>> GetStocktakingsByProductId(int productId);
        public Task<IEnumerable<Stocktaking>> GetStocktakingsByWarehouseId(int warehouseId);
    }
}
