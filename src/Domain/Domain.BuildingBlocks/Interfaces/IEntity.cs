namespace CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces
{
    public interface IEntity<Key>
    {
        /// <summary>
        /// Entity Identifier
        /// </summary>
        public Key Id { get; }
    }
}
