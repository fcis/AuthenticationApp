using AuthAPI.Domain.Entities;
using System.Security.Claims;

namespace AuthAPI.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Repository interface for application user management operations
    /// Extends the generic repository with user-specific operations
    /// </summary>
    public interface IUserRepository : IGenericRepository<ApplicationUser>
    {
        /// <summary>
        /// Gets a user by their email address
        /// </summary>
        /// <param name="email">The email address to search for</param>
        /// <returns>The user if found, null otherwise</returns>
        Task<ApplicationUser> GetByEmailAsync(string email);

        /// <summary>
        /// Gets a user by their username
        /// </summary>
        /// <param name="username">The username to search for</param>
        /// <returns>The user if found, null otherwise</returns>
        Task<ApplicationUser> GetByUsernameAsync(string username);

        /// <summary>
        /// Creates a new user with the specified password
        /// </summary>
        /// <param name="user">The user to create</param>
        /// <param name="password">The password for the new user</param>
        /// <returns>True if creation was successful, false otherwise</returns>
        Task<bool> CreateUserAsync(ApplicationUser user, string password);

        /// <summary>
        /// Validates a user's password
        /// </summary>
        /// <param name="user">The user to check</param>
        /// <param name="password">The password to validate</param>
        /// <returns>True if password is valid, false otherwise</returns>
        Task<bool> CheckPasswordAsync(ApplicationUser user, string password);

        /// <summary>
        /// Gets all roles assigned to a user
        /// </summary>
        /// <param name="user">The user to check</param>
        /// <returns>List of role names assigned to the user</returns>
        Task<IList<string>> GetRolesAsync(ApplicationUser user);

        /// <summary>
        /// Adds a user to a role
        /// </summary>
        /// <param name="user">The user to add to the role</param>
        /// <param name="role">The role to add the user to</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> AddToRoleAsync(ApplicationUser user, string role);

        /// <summary>
        /// Removes a user from a role
        /// </summary>
        /// <param name="user">The user to remove from the role</param>
        /// <param name="role">The role to remove the user from</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> RemoveFromRoleAsync(ApplicationUser user, string role);

        /// <summary>
        /// Gets all claims for a user
        /// </summary>
        /// <param name="user">The user to get claims for</param>
        /// <returns>List of claims assigned to the user</returns>
        Task<IList<Claim>> GetClaimsAsync(ApplicationUser user);

        /// <summary>
        /// Adds a claim to a user
        /// </summary>
        /// <param name="user">The user to add the claim to</param>
        /// <param name="claim">The claim to add</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> AddClaimAsync(ApplicationUser user, Claim claim);

        /// <summary>
        /// Removes a claim from a user
        /// </summary>
        /// <param name="user">The user to remove the claim from</param>
        /// <param name="claim">The claim to remove</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> RemoveClaimAsync(ApplicationUser user, Claim claim);
    }
}