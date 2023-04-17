using Microsoft.EntityFrameworkCore;
using StudentApi.Entities;
using System.Linq.Expressions;

namespace StudentApi.Repositories
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        private readonly DbContext _context;
        private readonly DbSet<T> _entity;

        public Repository(DbContext dbContext)
        {
            _context = dbContext;
            _entity = _context.Set<T>();
        }

        public Task<List<T>> GetAllAsync()
        {
            return _entity.ToListAsync();
        }

        public Task<T?> GetByIdAsync(int id)
        {
            return _entity.FirstOrDefaultAsync(t => t.Id == id);
        }

        public Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return _entity.Where(predicate).ToListAsync();
        }

        public Task<int> AddAsync(T item)
        {
            _entity.AddAsync(item);
            return _context.SaveChangesAsync();
        }

        public Task<int> UpdateAsync(T item)
        {
            _context.Update(item);
            return _context.SaveChangesAsync();

        }

        public Task<int> DeleteAsync(T item)
        {
            _entity.Remove(item);
            return _context.SaveChangesAsync();
        }
    }
}
