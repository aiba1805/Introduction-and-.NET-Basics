using System;

namespace EFCore.Models
{
    public class Order : Entity
    {
        public decimal Amount { get; set; }
        public DateTime OrderDate { get; set; }
        public Guid CartId { get; set; }
        public virtual Cart Cart { get; set; }
    }
}