using Lombard.Domain.Enums;
using System;

namespace Lombard.Domain.Entities
{
    public abstract class BaseEntity
    {
        public long Id { get; set; }
        public RecordStatus RecordStatus { get; set; }
        public DateTime CreateAt { get; set; }
        public Guid? UserId { get; set; }
    }
}
