using DatabaseProject.DbContexts;
using DatabaseProject.Test.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Test.Fixtures
{
    public class DockerWebApplicationFactoryFixture : WebApplicationFactory<DatabaseProject.Program>, IAsyncLifetime
    {
        private MsSqlContainer _dbContainer;
        public int InitialStudentsCount { get; } = 3;

        public DockerWebApplicationFactoryFixture()
        {
            _dbContainer = new MsSqlBuilder()
                .WithDatabase("DataBaseName")
                .WithPassword("$tr0ngP@$$w0rd")
                .WithUsername("MyUser")
                .Build();
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var connectionString = _dbContainer.GetConnectionString();
            base.ConfigureWebHost(builder);
            builder.ConfigureTestServices(services =>
            {
                services.RemoveAll(typeof(DbContextOptions<SchoolDbContext>));
                services.AddDbContext<SchoolDbContext>(options =>
                {
                    options.UseSqlServer(connectionString);
                });
            });
        }

        public async Task InitializeAsync()
        {
            await _dbContainer.StartAsync();

            using (var scope = Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<SchoolDbContext>();

                await cntx.Database.EnsureCreatedAsync();

                await cntx.Students.AddRangeAsync(DataFixture.GetStudents(InitialStudentsCount));
                await cntx.SaveChangesAsync();
            }
        }

        public async Task DisposeAsync()
        {
            await _dbContainer.StopAsync();
        }
    }
}
