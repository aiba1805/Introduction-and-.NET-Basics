using System.Collections.Generic;

namespace EFCore.Models
{
    public class Seller : Entity
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }

        public virtual List<Product> Products { get; } = new List<Product>();
    }
}