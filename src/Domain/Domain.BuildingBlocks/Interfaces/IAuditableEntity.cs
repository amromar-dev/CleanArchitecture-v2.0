using CleanArchitectureTemplate.Domain.BuildingBlocks.ValueTypes;

namespace CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces
{
    public interface IAuditableEntity
    {
        /// <summary>
        /// Gets the auditing information for this entity.
        /// </summary>
        public Auditing Auditing { get; }
    }
}
