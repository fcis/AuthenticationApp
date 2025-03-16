using System.Security.Claims;

namespace AuthAPI.Domain.Interfaces
{
    /// <summary>
    /// Service interface for accessing the current authenticated user's information
    /// </summary>
    public interface ICurrentUserService
    {
        /// <summary>
        /// Gets the current user's ID
        /// </summary>
        string? UserId { get; }

        /// <summary>
        /// Gets the current user's username
        /// </summary>
        string? UserName { get; }

        /// <summary>
        /// Gets all claims associated with the current user
        /// </summary>
        IEnumerable<Claim> Claims { get; }

        /// <summary>
        /// Indicates whether the current user is authenticated
        /// </summary>
        bool IsAuthenticated { get; }

        /// <summary>
        /// Checks if the current user is in a specific role
        /// </summary>
        /// <param name="role">The role to check</param>
        /// <returns>True if the user is in the role, false otherwise</returns>
        bool IsInRole(string role);
    }
}