
namespace CleanArchitectureTemplate.Application.BuildingBlocks.Contracts.Identity
{
    public interface IIdentityContext
    {
        /// <summary>
        /// Gets the full name of the current user.
        /// </summary>
        /// <returns>The user's full name.</returns>
        string GetFullName();

        /// <summary>
        /// Gets the ID of the current user.
        /// </summary>
        /// <returns>The user's ID as an integer.</returns>
        int GetUserId();

        /// <summary>
        /// Gets the username of the current user.
        /// </summary>
        /// <returns>The user's username.</returns>
        string GetUserName();

        /// <summary>
        /// Gets the list of roles assigned to the current user.
        /// </summary>
        /// <returns>A list of roles.</returns>
        List<string> GetRoles();

        /// <summary>
        /// Checks if the current user has the specified role.
        /// </summary>
        /// <param name="role">The role to check.</param>
        /// <returns>True if the user has the role, otherwise false.</returns>
        bool HasRole(string role);

        /// <summary>
        /// Gets the organization id of the current user.
        /// </summary>
        /// <returns>The user's username.</returns>
        int? GetOrganizationId();

        /// <summary>
        /// Gets the candidate id of the current user.
        /// </summary>
        /// <returns>The user's username.</returns>
        int? GetCandidateId();

        /// <summary>
        /// Get boolean if the loggedin user system admin
        /// </summary>
        /// <returns></returns>
        bool SystemAdmin();

        /// <summary>
        /// Get boolean if the loggedin user system admin
        /// </summary>
        /// <returns></returns>
        bool Candidate();
    }
}