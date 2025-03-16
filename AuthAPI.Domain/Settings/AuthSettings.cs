namespace AuthAPI.Domain.Settings
{
    /// <summary>
    /// Configuration settings for authentication and authorization
    /// </summary>
    public class AuthSettings
    {
        /// <summary>
        /// Secret key used for signing JWT tokens
        /// </summary>
        public string Key { get; set; } = string.Empty;

        /// <summary>
        /// Token issuer (typically your application name or URL)
        /// </summary>
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Token audience (who or what the token is intended for)
        /// </summary>
        public string Audience { get; set; } = string.Empty;

        /// <summary>
        /// Standard token expiry time in minutes
        /// </summary>
        public double TokenExpiryInMinutes { get; set; } = 60;

        /// <summary>
        /// Extended token expiry time in minutes (used for "remember me" functionality)
        /// </summary>
        public double ExtendedTokenExpiryInMinutes { get; set; } = 1440; // 24 hours

        /// <summary>
        /// Refresh token expiry time in days
        /// </summary>
        public double RefreshTokenExpiryInDays { get; set; } = 7;

        /// <summary>
        /// Maximum allowed failed login attempts before account is temporarily blocked
        /// </summary>
        public int MaxLoginAttempts { get; set; } = 3;

        /// <summary>
        /// Duration in minutes for which an account remains blocked after exceeding max login attempts
        /// </summary>
        public int BlockDurationInMinutes { get; set; } = 15;
    }
}