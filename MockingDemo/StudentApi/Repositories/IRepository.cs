using StudentApi.Entities;
using System.Linq.Expressions;

namespace StudentApi.Repositories
{
    public interface IRepository<T> where T : class, IEntity
    {
        Task<int> AddAsync(T item);
        Task<int> DeleteAsync(T item);
        Task<List<T>> GetAsync(Expression<Func<T, bool>> predicate);
        Task<T?> GetByIdAsync(int dd);
        Task<List<T>> GetAllAsync();
        Task<int> UpdateAsync(T item);
    }
}