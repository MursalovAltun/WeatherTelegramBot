using System;

namespace Common.Entities
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }

        public virtual DateTime CreationDate { get; set; }

        public virtual DateTime ModifyDate { get; set; }

        public bool IsDelete { get; set; }
    }
}