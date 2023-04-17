using System;
using System.Collections.Generic;

namespace StudentApi.Entities
{
    public partial class Student : IEntity
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
    }
}
