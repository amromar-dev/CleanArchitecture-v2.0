namespace CleanArchitectureTemplate.Domain.BuildingBlocks.Interfaces
{
    /// <summary>
    /// Defines a contract for business rules. Implementations should provide logic
    /// to determine if a rule is broken and a message describing the rule.
    /// </summary>
    public interface IBusinessRule
    {
        /// <summary>
        /// Determines whether the business rule is broken.
        /// </summary>
        /// <returns>True if the rule is broken; otherwise, false.</returns>
        bool IsBroken();

        /// <summary>
        /// Gets the message associated with the business rule.
        /// </summary>
        /// <value>The message that describes the business rule.</value>
        string Message { get; }
    }
}
