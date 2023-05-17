using DatabaseProject.DbContexts;
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
    public class WebApplicationFactoryFixture : IAsyncLifetime
    {
        private const string _connectionString = @$"Server=(localdb)\.;Database=UserIntegration;Trusted_Connection=True";

        private WebApplicationFactory<Program> _factory;

        public HttpClient Client { get; private set; }
        public int InitialStudentsCount { get; set; } = 3;

        public WebApplicationFactoryFixture()
        {
            _factory = new WebApplicationFactory<DatabaseProject.Program>().WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(Services =>
                {
                    Services.RemoveAll(typeof(DbContextOptions<SchoolDbContext>));
                    Services.AddDbContext<SchoolDbContext>(options =>
                    {
                        options.UseSqlServer(_connectionString);
                    });
                });
            });
            Client = _factory.CreateClient();
        }

        async Task IAsyncLifetime.DisposeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<SchoolDbContext>();

                await cntx.Database.EnsureDeletedAsync();
            }
        }

        async Task IAsyncLifetime.InitializeAsync()
        {
            using (var scope = _factory.Services.CreateScope())
            {
                var scopedServices = scope.ServiceProvider;
                var cntx = scopedServices.GetRequiredService<SchoolDbContext>();

                await cntx.Database.EnsureCreatedAsync();

                await cntx.Students.AddRangeAsync(DataFixture.GetStudents(InitialStudentsCount));
                await cntx.SaveChangesAsync();
            }
        }
    }
}
