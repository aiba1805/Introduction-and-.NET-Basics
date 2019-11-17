using System;
using System.Collections.Generic;

namespace EFCore.Models
{
    public class Cart : Entity
    {
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual List<ProductsCarts> ProductsCarts { get; } = new List<ProductsCarts>();
    }
}