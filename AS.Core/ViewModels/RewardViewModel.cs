using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace AS.Core.ViewModels
{
    public class RewardViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [MaxLength(50)]
        [RegularExpression(@"[A-Za-z0-9\s-]*")]
        public string Title { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        public string ImagePath { get; set; }

        [DataType(DataType.Upload)]
        [Required]
        public IFormFile Image { get; set; }
    }
}