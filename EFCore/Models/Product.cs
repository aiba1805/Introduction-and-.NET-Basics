using System;
using System.Collections.Generic;

namespace EFCore.Models
{
    public class Product : Entity
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        
        public Guid SellerId { get; set; }
        public virtual Seller Seller { get; set; }
        
        public virtual List<ProductsCarts> ProductsCarts { get; } = new List<ProductsCarts>();

        public virtual List<Comment> Comments { get; } = new List<Comment>();
    }
}