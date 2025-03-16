namespace AuthAPI.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for JWT authentication tokens
    /// </summary>
    public class TokenDto
    {
        /// <summary>
        /// The JWT access token
        /// </summary>
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// The refresh token used to obtain a new access token
        /// </summary>
        public string RefreshToken { get; set; } = string.Empty;

        /// <summary>
        /// The expiration date of the access token
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// The username of the authenticated user
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// List of roles assigned to the user
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();

        /// <summary>
        /// Custom claims assigned to the user
        /// </summary>
        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
    }
}