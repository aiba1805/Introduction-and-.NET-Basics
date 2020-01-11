using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace AS.Core.Models
{
    public class User : IdentityUser<Guid>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public DateTime BirthDate { get; set; }
        public string PhotoPath { get; set; }
        public virtual List<UserReward> Rewards { get; }

        public User()
        {
            Rewards=new List<UserReward>();
        }
    }
}