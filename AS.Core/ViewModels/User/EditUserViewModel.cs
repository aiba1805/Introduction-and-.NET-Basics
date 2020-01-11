using System;
using System.ComponentModel.DataAnnotations;
using AS.Core.Attributes;
using Microsoft.AspNetCore.Http;

namespace AS.Core.ViewModels
{
    public class EditUserViewModel 
    {
        public Guid Id { get; set; }
    
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Lastname is required")]
        [MaxLength(50)]
        public string LastName { get; set; }
        
        [Required(ErrorMessage = "Birth Date is required")]
        [DisplayFormat(DataFormatString = "{0:MMM/d/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [BirthDateValidation]
        public DateTime BirthDate { get; set; }
        
        public string PhotoPath { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
    }
}