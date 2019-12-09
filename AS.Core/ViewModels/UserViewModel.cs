using System;
using System.ComponentModel.DataAnnotations;

namespace AS.Core.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; }
        
        [Required]
        public DateTime BirthDate { get; set; }
        
        public int Age => (int)((DateTime.Now - BirthDate).TotalDays / 365);
        public string PhotoPath { get; set; }

    }
}