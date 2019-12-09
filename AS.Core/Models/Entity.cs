using System;

namespace AS.Core.Models
{
    public class Entity
    {
        public Guid Id { get; set; }

        public Entity()
        {
            Id = new Guid();
        }
    }
}