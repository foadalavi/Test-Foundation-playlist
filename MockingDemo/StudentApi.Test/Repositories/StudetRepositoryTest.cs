using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
using StudentApi.DbContexts;
using StudentApi.Entities;
using StudentApi.Repositories;
using StudentApi.Test.Fixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApi.Test.Repositories
{
    public class StudetRepositoryTest
    {
        [Fact]
        public async Task OnCallingGetAllAsync_WhenSuccess_ShouldReturn3Results()
        {
            // Arrenge
            var dbContextMock = new Mock<StudentDbContext>();
            dbContextMock.Setup(t => t.Students).ReturnsDbSet(DataFixture.GetAllStudents());
            var repo = new StudentRepository(dbContextMock.Object);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            result.Count.Should().Be(3);
        }


        [Fact]
        public async Task OnCallingGetAllAsync_WhenSuccess_ShouldReturnTheExpextedresult()
        {
            // Arrenge
            var dbContextMock = new Mock<StudentDbContext>();
            dbContextMock.Setup(t => t.Students).ReturnsDbSet(DataFixture.GetAllStudents());
            var repo = new StudentRepository(dbContextMock.Object);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            result.Should().BeEquivalentTo(DataFixture.GetAllStudents());
        }
    }
}
