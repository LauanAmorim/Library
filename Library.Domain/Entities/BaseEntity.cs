using Library.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public DateTimeOffset? ModifiedAt { get; protected set; }
        public DateTimeOffset? DeletedAt { get; protected set; }
        public EntityStatus Status { get; set; }

        protected BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Status = EntityStatus.Active;
        }

        public void Update()
        {
            ModifiedAt = DateTime.UtcNow;
        }
        public void Delete()
        {
            if (Status == EntityStatus.Inactive) throw new InvalidOperationException("Entity already Inactive");
            Status = EntityStatus.Inactive;
            DeletedAt = DateTime.UtcNow;
            Update();
        }
        public void Restore()
        {
            if (Status == EntityStatus.Active) throw new InvalidOperationException("Entity already Active");
            Status = EntityStatus.Active;
            ModifiedAt = DateTime.UtcNow;
            Update();
        }
    }
}
