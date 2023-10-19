using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutoMapperAPI
{
    public class User
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public Address? Address { get; set; }

        public string GetFullName()
        { 
            return $"{LastName}, {FirstName}";
        }
    }
}