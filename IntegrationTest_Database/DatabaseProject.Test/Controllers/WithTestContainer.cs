using DatabaseProject.Models;
using DatabaseProject.Test.Fixtures;
using DatabaseProject.Test.Helper;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Test.Controllers
{
    public class WithTestContainer: IClassFixture<DockerWebApplicationFactoryFixture>
    {
        private readonly DockerWebApplicationFactoryFixture _factory;
        private readonly HttpClient _client;

        public WithTestContainer(DockerWebApplicationFactoryFixture factory)
        {
            _factory = factory;
            _client = _factory.CreateClient();
        }

        //[Fact]
        //public async Task OnGetStudent_WhenExecuteController_ShouldreturnTheExpecedStudet()
        //{
        //    // Arrange

        //    // Act
        //    var response = await _client.GetAsync(HttpHelper.Urls.GetAllStudents);
        //    var result = await response.Content.ReadFromJsonAsync<List<Student>>();

        //    // Assert
        //    response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);

        //    result.Count.Should().Be(_factory.InitialStudentsCount);
        //    result.Should()
        //        .BeEquivalentTo(DataFixture.GetStudents(_factory.InitialStudentsCount), options => options.Excluding(t => t.StudentId));
        //}

        [Fact]
        public async Task OnAddStudent_WhenExecuteController_ShouldStoreInDb()
        {
            // Arrange
            var newStudent = DataFixture.GetStudent(true);

            // Act
            var request = await _client.PostAsync(HttpHelper.Urls.AddStudent, HttpHelper.GetJsonHttpContent(newStudent));
            var response = await _client.GetAsync($"{HttpHelper.Urls.GetStudent}/{_factory.InitialStudentsCount + 1}");
            var result = await response.Content.ReadFromJsonAsync<Student>();

            // Assert
            request.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);


            result.FirstName.Should().Be(newStudent.FirstName);
            result.LastName.Should().Be(newStudent.LastName);
            result.Address.Should().Be(newStudent.Address);
            result.BirthDay.Should().Be(newStudent.BirthDay);
        }
    }
}
