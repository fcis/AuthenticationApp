using Microsoft.AspNetCore.Identity;

namespace AuthAPI.Domain.Entities
{
    /// <summary>
    /// Application user entity that extends the ASP.NET Core Identity user
    /// Adds additional properties needed for the authentication system
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Indicates if the user account is temporarily blocked
        /// </summary>
        public bool IsBlocked { get; set; } = false;

        /// <summary>
        /// Number of consecutive failed login attempts
        /// </summary>
        public int LoginFailedCount { get; set; } = 0;

        /// <summary>
        /// Date and time until which the user account is blocked
        /// </summary>
        public DateTime? BlockedUntil { get; set; }

        /// <summary>
        /// Date and time of the last successful login
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Refresh token for JWT authentication
        /// </summary>
        public string? RefreshToken { get; set; }

        /// <summary>
        /// Expiry time for the refresh token
        /// </summary>
        public DateTime? RefreshTokenExpiryTime { get; set; }

        /// <summary>
        /// User's first name
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// User's last name
        /// </summary>
        public string? LastName { get; set; }
    }
}