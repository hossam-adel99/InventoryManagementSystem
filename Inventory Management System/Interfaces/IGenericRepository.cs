namespace Quiz.Interface
{
    public interface IGenericRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T obj);
        Task RemoveAsync(int id);
        Task UpdateAsync(T obj);
        Task SaveAsync();
    }

}
