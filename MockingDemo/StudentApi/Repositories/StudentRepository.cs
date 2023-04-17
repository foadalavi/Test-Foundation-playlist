using Microsoft.EntityFrameworkCore;
using StudentApi.DbContexts;
using StudentApi.Entities;

namespace StudentApi.Repositories
{
    public class StudentRepository
    {
        private StudentDbContext _context;

        public StudentRepository(StudentDbContext dbContext)
        {
            _context = dbContext;
        }

        public Task<List<Student>> GetAllAsync()
        {
            return _context.Students.ToListAsync();
        }
    }
}
