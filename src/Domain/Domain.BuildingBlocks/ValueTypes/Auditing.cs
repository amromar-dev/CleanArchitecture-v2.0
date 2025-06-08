using CleanArchitectureTemplate.SharedKernels.Exceptions;
using CleanArchitectureTemplate.SharedKernels.Localizations;

namespace CleanArchitectureTemplate.Domain.BuildingBlocks.ValueTypes
{
    /// <summary>
    /// Represents auditing information for an entity, including creation, modification, and deletion details.
    /// </summary>
    /// <remarks>
    /// Initializes a new instance of the <see cref="Auditing"/> class.
    /// </remarks>
    /// <param name="actionUserId">The ID of the user who performed the action.</param>
    public class Auditing
    {
        private Auditing()
        {
                
        }

        public Auditing(int actionUserId = default)
        {
            CreatedAt= DateTime.UtcNow;
            CreatedBy = actionUserId;
        }

        /// <summary>
        /// Gets the date and time when the entity was created.
        /// </summary>
        public DateTimeOffset CreatedAt { get; private set; }

        /// <summary>
        /// Gets the ID of the user who created the entity.
        /// </summary>
        public int CreatedBy { get; private set; }

        /// <summary>
        /// Gets the date and time when the entity was last modified.
        /// </summary>
        public DateTimeOffset? ModifiedAt { get; private set; }

        /// <summary>
        /// Gets the ID of the user who last modified the entity.
        /// </summary>
        public int? ModifiedBy { get; private set; }

        /// <summary>
        /// Gets the date and time when the entity was deleted.
        /// </summary>
        public DateTimeOffset? DeletedAt { get; private set; }

        /// <summary>
        /// Gets the ID of the user who deleted the entity.
        /// </summary>
        public int? DeletedBy { get; private set; }

        /// <summary>
        /// Gets a value indicating whether the entity is deleted.
        /// </summary>
        public bool IsDeleted { get; private set; } = false; // Default to not deleted

        /// <summary>
        /// Updates the auditing information to reflect that the entity has been modified.
        /// </summary>
        /// <param name="actionUserId">The ID of the user who performed the modification.</param>
        public void LogModification(int actionUserId)
        {
            if (IsDeleted)
                throw new BusinessRuleException(Localization.InvalidOperationException);

            ModifiedAt = DateTime.UtcNow;
            ModifiedBy = actionUserId;
        }

        /// <summary>
        /// Updates the auditing information to reflect that the entity has been deleted.
        /// </summary>
        /// <param name="actionUserId">The ID of the user who performed the deletion.</param>
        public void Delete(int actionUserId)
        {
            if (IsDeleted)
                throw new BusinessRuleException(Localization.InvalidOperationException);

            IsDeleted = true;
            DeletedAt = DateTime.UtcNow;
            DeletedBy = actionUserId;
        }

        /// <summary>
        /// Updates the auditing information to reflect that the entity has been deleted.
        /// </summary>
        /// <param name="actionUserId">The ID of the user who performed the deletion.</param>
        public void Restore(int actionUserId)
        {
            if (IsDeleted == false)
                throw new BusinessRuleException(Localization.InvalidOperationException);

            IsDeleted = false;
            DeletedAt = null;
            DeletedBy = null;

            LogModification(actionUserId);
        }
    }
}
