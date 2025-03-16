using AuthAPI.Domain.Entities;
using System.Security.Claims;

namespace AuthAPI.Domain.Interfaces
{
    /// <summary>
    /// Service interface for JWT token operations
    /// </summary>
    public interface IJwtService
    {
        /// <summary>
        /// Generates a JWT token for a user
        /// </summary>
        /// <param name="user">The user to generate a token for</param>
        /// <param name="extendedExpiry">Whether to use extended token expiry time</param>
        /// <returns>Token object containing access and refresh tokens</returns>
        Task<object> GenerateTokenAsync(ApplicationUser user, bool extendedExpiry = false);

        /// <summary>
        /// Generates a cryptographically secure refresh token
        /// </summary>
        /// <returns>The refresh token as a string</returns>
        string GenerateRefreshToken();

        /// <summary>
        /// Extracts and validates the claims principal from an expired JWT token
        /// </summary>
        /// <param name="token">The expired JWT token</param>
        /// <returns>The claims principal from the token</returns>
        ClaimsPrincipal GetPrincipalFromExpiredToken(string token);
    }
}