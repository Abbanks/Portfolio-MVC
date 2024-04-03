using Microsoft.EntityFrameworkCore;
using Portfolio.Data;
using Portfolio.Services.Interfaces;

namespace Portfolio.Services
{
    public class GenericEntityRepositoryService<T> : IGenericEntityRepositoryService<T> where T : class
    {
        private readonly PortfolioDbContext _context;

        public GenericEntityRepositoryService(PortfolioDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }


        public async Task<bool> UpdateAsync(T entity)
        {
            _context.Set<T>().Update(entity);
            return await _context.SaveChangesAsync() > 0;
        }

         
    }
}
