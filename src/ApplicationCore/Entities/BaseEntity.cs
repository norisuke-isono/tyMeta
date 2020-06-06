using System;

namespace ApplicationCore.Entites
{
    public abstract class BaseEntity
    {
        public virtual int Id { get; set; }
        public virtual string CreateUserId { get; set; }
        public virtual string UpdateUserId { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual DateTime UpdatedAt { get; set; }
    }
}