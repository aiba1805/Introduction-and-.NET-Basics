using System.Collections.Generic;

namespace EFCore.Models
{
    public class Customer : Entity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual List<Comment> Comments { get; } = new List<Comment>();
    }
}