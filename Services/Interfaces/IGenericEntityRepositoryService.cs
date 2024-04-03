namespace Portfolio.Services.Interfaces
{
    public interface IGenericEntityRepositoryService<T> where T : class
    {
        Task<bool> AddAsync(T entity);
        Task<bool> DeleteAsync(T entity);
        Task<IEnumerable<T>> GetAllAsync();
        Task<bool> UpdateAsync(T entity);
         
    }
}
