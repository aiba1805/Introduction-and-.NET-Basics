using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AS.Core.Models;

namespace AS.Core.ViewModels
{
    public class AddRewardViewModel
    {
        public List<Reward> Rewards { get; set; }
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Reward is required")]
        public Guid RewardId { get; set; }
    }
}