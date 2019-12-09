using System;
using Microsoft.AspNetCore.Http;

namespace AS.Core.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhotoPath { get; set; }
    }
}