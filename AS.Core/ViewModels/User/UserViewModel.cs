using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using AS.Core.Attributes;
using AS.Core.Models;
using Faisalman.AgeCalc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AS.Core.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "First Name is required")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        
        [Required(ErrorMessage = "Lastname is required")]
        [MaxLength(50)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Birth Date is required")]
        [DisplayFormat(DataFormatString = "{0:MMM/d/yyyy}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        [BirthDateValidation]
        public DateTime BirthDate { get; set; }

        public int Age
        {
            get
            {
                var calc = new Age(BirthDate, DateTime.Now);
                return calc.Years;
            }
        }
        public List<Reward> Rewards { get; set; }

        public string PhotoPath { get; set; }

        [DataType(DataType.Upload)]
        public IFormFile Photo { get; set; }
    }
}