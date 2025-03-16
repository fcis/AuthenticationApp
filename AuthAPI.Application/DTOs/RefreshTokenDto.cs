using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for token refresh operations
    /// </summary>
    public class RefreshTokenDto
    {
        /// <summary>
        /// The expired access token
        /// </summary>
        [Required(ErrorMessage = "Access token is required")]
        public string AccessToken { get; set; } = string.Empty;

        /// <summary>
        /// The refresh token
        /// </summary>
        [Required(ErrorMessage = "Refresh token is required")]
        public string RefreshToken { get; set; } = string.Empty;
    }
}