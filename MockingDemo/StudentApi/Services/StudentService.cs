using Microsoft.EntityFrameworkCore;
using StudentApi.Entities;
using StudentApi.Repositories;
using System.Linq.Expressions;

namespace StudentApi.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _repository;

        public StudentService(IRepository<Student> repository)
        {
            _repository = repository;
        }

        public Task<List<Student>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<List<Student>> GetAsync(Expression<Func<Student, bool>> predicate)
        {
            return _repository.GetAsync(predicate);
        }

        public async Task<bool> AddAsync(Student item)
        {
            var result = await _repository.AddAsync(item);
            return (result > 0);
        }

        public async Task<bool> UpdateAsync(Student item)
        {
            var student = await _repository.GetByIdAsync(item.Id);
            if (student is null)
            {
                throw new NullReferenceException($"There is no student with ID ={item.Id}");
            }
            student.FirstName = item.FirstName;
            student.LastName = item.LastName;

            var result = await _repository.UpdateAsync(student);
            return (result > 0);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            if (student is null)
            {
                throw new NullReferenceException($"There is no student with ID ={id}");
            }
            var result = await _repository.DeleteAsync(student);
            return (result > 0);
        }
    }
}
