using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentApi.DbContexts;
using StudentApi.Entities;
using StudentApi.Services;

namespace StudentApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudentsAsync()
        {
            var students = await _studentService.GetAllAsync();
            return Ok(students);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentAsync(int id)
        {
            var student = await _studentService.GetAsync(t => t.Id == id);
            if (student is null)
            {
                return BadRequest("No result");
            }
            return Ok(student);
        }

        [HttpPost("AddStudent")]
        public async Task<ActionResult<Student>> AddStudentAsync(Student student)
        {
            var result = await _studentService.AddAsync(student);
            if (result)
            {
                return Ok();
            }
            return BadRequest("An unexpected error happened!");
        }


        [HttpPut("EditStudent")]
        public async Task<ActionResult<Student>> UpdateStudentAsync(Student editedStudent)
        {
            var result = await _studentService.UpdateAsync(editedStudent);
            if (result)
            {
                return Ok();
            }
            return BadRequest("An unexpected error happened!");
        }


        [HttpDelete("DeleteStudent/{id}")]
        public async Task<IActionResult> UpdateStudentAsync(int id)
        {

            var result = await _studentService.DeleteAsync(id);
            if (result)
            {
                return Ok();
            }
            return BadRequest("An unexpected error happened!");
        }

    }
}
