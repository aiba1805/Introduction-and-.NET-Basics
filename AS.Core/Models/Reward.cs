using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace AS.Core.Models
{
    public class Reward : Entity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; }
        public virtual List<UserReward> Users { get; }

        public Reward()
        {
            Users = new List<UserReward>();
        }
    }
}