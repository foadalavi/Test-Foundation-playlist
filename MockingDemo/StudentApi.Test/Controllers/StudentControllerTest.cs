using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentApi.Controllers;
using StudentApi.Entities;
using StudentApi.Services;
using StudentApi.Test.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApi.Test.Controllers
{
    public class StudentControllerTest
    {
        private readonly StudentController controller;

        public StudentControllerTest()
        {
            var studentServiceMock = new Mock<IStudentService>();
            controller = new StudentController(studentServiceMock.Object);
            studentServiceMock.Setup(t => t.GetAllAsync()).ReturnsAsync(DataFixture.GetAllStudents());
        }

        [Fact]
        public async Task OnGetStudentsAsync_WhenSuccess_shouldReturn3Results()
        {
            // Arragne

            // Act

            var response = (OkObjectResult)(await controller.GetStudentsAsync()).Result;
            var responseresult = (List<Student>)response.Value;

            // Assert

            Assert.Equal(3, responseresult.Count);
        }

        [Fact]
        public async Task OnGetStudentsAsync_WhenSuccess_ShouldReturnresultUsingStatus200()
        {
            // Arragne

            // Act

            var response = (OkObjectResult)(await controller.GetStudentsAsync()).Result;

            // Assert

            Assert.Equal(200, response.StatusCode);
        }
    }
}
