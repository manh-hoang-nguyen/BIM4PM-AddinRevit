namespace BIM4PM.Model
{
    using System;

    public abstract class EntityBase
    {
        public string _id { get; set; }

        public DateTime createdAt { get; set; }

        public DateTime updatedAt { get; set; }

        public int __v { get; set; }

        public bool IsValid => Validate();
 
        public abstract bool Validate();
    }
}
