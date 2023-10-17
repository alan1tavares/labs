using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperStudy.Models
{
    public class StudentDT
    {
        public required string Name { get; set; }
        public int Age { get; set; }
        public string? Address { get; set; }
        public string? Department { get; set; }
    }
}