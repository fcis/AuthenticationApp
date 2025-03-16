namespace AuthAPI.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for user information
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The user's unique identifier
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The user's username
        /// </summary>
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// The user's email address
        /// </summary>
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// The user's first name
        /// </summary>
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The user's last name
        /// </summary>
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Flag indicating if the user account is blocked
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Date and time until which the account is blocked
        /// </summary>
        public DateTime? BlockedUntil { get; set; }

        /// <summary>
        /// Date and time of the last successful login
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// List of roles assigned to the user
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();

        /// <summary>
        /// Dictionary of claims assigned to the user
        /// </summary>
        public Dictionary<string, string> Claims { get; set; } = new Dictionary<string, string>();
    }
}