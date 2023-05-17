using DatabaseProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace DatabaseProject.DbContexts
{
    public class SchoolDbContext : DbContext
    {
        public SchoolDbContext(DbContextOptions<SchoolDbContext> options) : base(options)
        {

        }

        public DbSet<Student> Students { get; set; }
    }
}
