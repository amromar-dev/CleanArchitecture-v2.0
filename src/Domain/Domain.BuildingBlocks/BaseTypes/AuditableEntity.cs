using CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces;
using CleanArchitectureTemplate.Domain.BuildingBlocks.ValueTypes;

namespace CleanArchitectureTemplate.Domain.BuildingBlocks.BaseTypes
{
    /// <summary>
    /// Abstract base class for entities that require auditing functionality.
    /// </summary>
    public abstract class AuditableEntity<Key> : Entity<Key>, IAuditableEntity
    {
        /// <summary>
        /// Gets the auditing information for this entity.
        /// </summary>
        public Auditing Auditing { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuditableEntity"/> class.
        /// </summary>
        /// <param name="actionUserId">The ID of the user performing the action.</param>
        protected AuditableEntity(int actionUserId = default)
        {
            Auditing = new Auditing(actionUserId);
        }

        /// <summary>
        /// Updates the auditing information to reflect that the entity has been modified.
        /// </summary>
        /// <param name="actionUserId">The ID of the user who performed the modification.</param>
        protected virtual void LogModification(int actionUserId)
        {
            Auditing.LogModification(actionUserId);
        }

        /// <summary>
        /// Updates the auditing information to reflect that the entity has been deleted.
        /// </summary>
        /// <param name="actionUserId">The ID of the user who performed the deletion.</param>
        protected virtual void Delete(int actionUserId)
        {
            Auditing.Delete(actionUserId);
        }
    }
}
