using StudentApi.Entities;
using StudentApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentApi.Test.Fixture
{
    internal class DataFixture
    {
        internal static List<Student> GetAllStudents()
        {
            return new List<Student>
            {
                new Student
                {
                    Id = 1,
                    FirstName = "Sansa",
                    LastName = "Stark"
                },
                new Student
                {
                    Id = 2,
                    FirstName = "Arya",
                    LastName = "Stark"
                },
                new Student
                {
                    Id = 3,
                    FirstName = "Robb",
                    LastName = "Stark"
                }
            };
        }

        internal static Product GetDummyProduct()
        {
            return new Product
            {
                Id = 1,
                Title = "Product 1",
                Description = "An apple mobile which is nothing like apple",
                Price = 500,
                DiscountPercentage = 7.5f,
                Rating = 3.5f,
                Stock = 86,
                Brand = "Dummy Company",
                Category = "Dummy Category",
                Thumbnail = "thumbnail 1",
                Images = new[]
                {
                    "url 1",
                    "url 2",
                    "url 3"
                }
            };
        }
    }
}
