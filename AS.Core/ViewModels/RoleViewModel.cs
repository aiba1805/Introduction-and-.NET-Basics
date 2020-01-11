using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace AS.Core.ViewModels
{
    public class RoleViewModel
    {
        public Guid UserId { get; set; }
        public string UserEmail { get; set; }
        public List<IdentityRole<Guid>> AllRoles { get; set; }
        public IList<string> UserRoles { get; set; }
        
        public RoleViewModel()
        {
            AllRoles = new List<IdentityRole<Guid>>();
            UserRoles = new List<string>();
        }
    }
}