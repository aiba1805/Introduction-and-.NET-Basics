using System;

namespace EFCore.Models
{
    public class Comment : Entity
    {
        public string Content { get; set; }
        public Guid ProductId { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Product Product { get; set; }
        public virtual Customer Customer { get; set; }
    }
}