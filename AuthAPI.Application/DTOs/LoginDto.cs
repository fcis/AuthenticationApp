using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for user login
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// User's email address
        /// </summary>
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// User's password
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; } = string.Empty;

        /// <summary>
        /// Flag indicating whether to persist the login for an extended period
        /// </summary>
        public bool RememberMe { get; set; } = false;
    }
}