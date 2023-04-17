using Microsoft.EntityFrameworkCore;
using Moq;
using Moq.EntityFrameworkCore;
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
    public class RepositoryTest
    {
        [Fact]
        public async Task OnCallingGetAllAsync_WhenSuccess_ShouldReturn3Results()
        {
            // Arrenge
            var dbContextMock = new Mock<DbContext>();
            dbContextMock.Setup(t => t.Set<Student>()).ReturnsDbSet(DataFixture.GetAllStudents());
            var repo = new Repository<Student>(dbContextMock.Object);

            // Act
            var result = await repo.GetAllAsync();

            // Assert
            Assert.Equal(3, result.Count);
        }
    }
}
