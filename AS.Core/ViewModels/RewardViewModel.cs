using System;
using System.ComponentModel.DataAnnotations;

namespace AS.Core.ViewModels
{
    public class RewardViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(25)]
        public string Title { get; set; }
        [MaxLength(200)]
        public string Description { get; set; }
        
        public string ImagePath { get; set; }
    }
}