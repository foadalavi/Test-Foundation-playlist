using DatabaseProject.DbContexts;
using DatabaseProject.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Services
{
    public class StudentService : IStudentService
    {
        private readonly SchoolDbContext _context;

        public StudentService(SchoolDbContext context)
        {
            _context = context;
        }

        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }

        public async Task<Student> GetStudentAsync(int Id)
        {
            return await _context.Students.FindAsync(Id);
        }

        public async Task<bool> AddStudentAsync(Student newStudent)
        {
            _context.Students.Add(newStudent);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> EditStudentAsync(Student editedStudent)
        {
            _context.Update(editedStudent);
            var result = await _context.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> DeleteStudentAsync(int Id)
        {
            var student = await _context.Students.FindAsync(Id);
            if (student is null)
            {
                return false;
            }
            _context.Students.Remove(student);

            var result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
