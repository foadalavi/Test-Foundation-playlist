using Bogus;
using DatabaseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Test.Fixtures
{
    internal class DataFixture
    {
        public static List<Student> GetStudents(int count, bool useNewSeed = false)
        {
            return GetStudentFaker(useNewSeed).Generate(count);
        }
        public static Student GetStudent(bool useNewSeed = false)
        {
            return GetStudents(1, useNewSeed)[0];
        }

        private static Faker<Student> GetStudentFaker(bool useNewSeed)
        {
            var seed = 0;
            if (useNewSeed)
            {
                seed = Random.Shared.Next(10, int.MaxValue);
            }
            return new Faker<Student>()
                .RuleFor(t => t.StudentId, o => 0)
                .RuleFor(t => t.FirstName, (faker, t) => faker.Name.FirstName())
                .RuleFor(t => t.LastName, (faker, t) => faker.Name.LastName())
                .RuleFor(t => t.Address, (faker, t) => faker.Address.FullAddress())
                .RuleFor(t => t.BirthDay, (faker, t) => faker.Date.Past(5, new DateTime(2010, 1, 1)))
                .UseSeed(seed);
        }
    }
}
