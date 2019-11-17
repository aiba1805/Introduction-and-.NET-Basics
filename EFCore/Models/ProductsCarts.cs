using System;

namespace EFCore.Models
{
    public class ProductsCarts
    {
        public virtual Product Product { get; set; }
        public virtual Cart Cart { get; set; }
        public Guid ProductId { get; set; }
        public Guid CartId { get; set; }
    }
}