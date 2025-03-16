namespace AuthAPI.Domain.Interfaces
{
    /// <summary>
    /// Service interface for authentication operations
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Authenticates a user with email and password
        /// </summary>
        /// <param name="email">User's email</param>
        /// <param name="password">User's password</param>
        /// <param name="rememberMe">Whether to create a long-lived token</param>
        /// <returns>
        /// Tuple containing:
        /// - Whether the login was successful
        /// - A message describing the result
        /// - The token object if successful, null otherwise
        /// </returns>
        Task<(bool Succeeded, string Message, object? Token)> LoginAsync(string email, string password, bool rememberMe);

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="firstName">User's first name</param>
        /// <param name="lastName">User's last name</param>
        /// <param name="email">User's email</param>
        /// <param name="username">User's username</param>
        /// <param name="password">User's password</param>
        /// <returns>
        /// Tuple containing:
        /// - Whether the registration was successful
        /// - A message describing the result
        /// </returns>
        Task<(bool Succeeded, string Message)> RegisterAsync(string firstName, string lastName, string email, string username, string password);

        /// <summary>
        /// Refreshes an expired JWT token using a refresh token
        /// </summary>
        /// <param name="accessToken">The expired JWT token</param>
        /// <param name="refreshToken">The refresh token</param>
        /// <returns>
        /// Tuple containing:
        /// - Whether the token refresh was successful
        /// - A message describing the result
        /// - The new token object if successful, null otherwise
        /// </returns>
        Task<(bool Succeeded, string Message, object? Token)> RefreshTokenAsync(string accessToken, string refreshToken);

        /// <summary>
        /// Revokes a user's refresh token
        /// </summary>
        /// <param name="username">The username of the user</param>
        /// <returns>True if successful, false otherwise</returns>
        Task<bool> RevokeTokenAsync(string username);
    }
}