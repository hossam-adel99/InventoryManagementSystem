using Azure.Core;
using Inventory_Management_System.Interfaces;
using Inventory_Management_System.Models;
using Microsoft.EntityFrameworkCore;
using Quiz.Interface;

namespace Inventory_Management_System.Repositories
{
    public class ProductRepo : IProductRepo, IGenericRepository<Product>
    {
        private readonly MyContext myContext;

        public ProductRepo(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public async Task AddAsync(Product obj)
        {
            await myContext.Products.AddAsync(obj);
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await myContext.Products.Where(c=>c.IsDeleted == false).ToListAsync();
        }

        public async Task<IEnumerable<Product>> GetAllWithLowStockAsync()
        {
            return await myContext.Products.Where(c => c.IsDeleted == false && c.LowStockThreshold > c.Quantity).ToListAsync();
        }
        public async Task<IEnumerable<Product>> GetAllWithLowStockByCategoryAsync(int CaegorytId)
        {
            return await myContext.Products.Where(p => p.Quantity < p.LowStockThreshold &&  p.CategoryId == CaegorytId).ToListAsync();
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await myContext.Products.Where(c => c.Id == id && c.IsDeleted == false).FirstOrDefaultAsync();
        }

        public Task RemoveAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task SaveAsync()
        {
            await myContext.SaveChangesAsync();
        }
        public async Task UpdateAsync(Product entity)
        {
            myContext.Products.Update(entity);
        }

    }
}
