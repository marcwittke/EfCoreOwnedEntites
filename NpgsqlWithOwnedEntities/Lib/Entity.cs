using System;
using System.ComponentModel.DataAnnotations;
using JetBrains.Annotations;

namespace NpgsqlWithOwnedEntities.Lib
{
    public abstract class Entity : Identified
    {
        protected Entity()
        {
        }

        protected Entity(int id)
        {
            Id = id;
        }

        public DateTime CreatedOn { get; protected set; }

        [StringLength(100)] public string CreatedBy { get; protected set; }

        public DateTime? ChangedOn { get; protected set; }

        [StringLength(100)] public string ChangedBy { get; protected set; }

        public void SetCreatedProperties([NotNull] string createdBy, DateTime createdOn)
        {
            if (createdBy == null)
            {
                throw new ArgumentNullException(nameof(createdBy));
            }

            if (createdBy == string.Empty)
            {
                throw new ArgumentException(nameof(createdBy));
            }

            CreatedBy = createdBy;
            CreatedOn = createdOn;
        }

        public void SetModifiedProperties([NotNull] string changedBy, DateTime changedOn)
        {
            if (changedBy == null)
            {
                throw new ArgumentNullException(nameof(changedBy));
            }

            if (changedBy == string.Empty)
            {
                throw new ArgumentException(nameof(changedBy));
            }

            ChangedBy = changedBy;
            ChangedOn = changedOn;
        }
    }
}