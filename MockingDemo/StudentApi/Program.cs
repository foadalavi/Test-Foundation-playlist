using Microsoft.EntityFrameworkCore;
using StudentApi.DbContexts;
using StudentApi.Entities;
using StudentApi.Models.Config;
using StudentApi.Repositories;
using StudentApi.Services;

namespace StudentApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddDbContext<StudentDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("StudentDbConnection")));
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.Configure<ProductOptions>(builder.Configuration.GetSection("ProductOptions"));
            builder.Services.AddSwaggerGen();
            builder.Services
                .AddHttpClient()
                .AddTransient<DbContext, StudentDbContext>()
                .AddTransient<IRepository<Student>, Repository<Student>>()
                .AddTransient<IStudentService, StudentService>()
                .AddTransient<IProductService, ProductService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseCors();

            app.MapControllers();

            app.Run();
        }
    }
}