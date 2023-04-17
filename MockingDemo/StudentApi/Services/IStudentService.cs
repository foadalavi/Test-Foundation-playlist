using StudentApi.Entities;
using System.Linq.Expressions;

namespace StudentApi.Services
{
    public interface IStudentService
    {
        Task<bool> AddAsync(Student item);
        Task<bool> DeleteAsync(int id);
        Task<List<Student>> GetAllAsync();
        Task<List<Student>> GetAsync(Expression<Func<Student, bool>> predicate);
        Task<bool> UpdateAsync(Student item);
    }
}