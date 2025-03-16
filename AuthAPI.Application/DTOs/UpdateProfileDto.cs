using System.ComponentModel.DataAnnotations;

namespace AuthAPI.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for updating user profile information
    /// </summary>
    public class UpdateProfileDto
    {
        /// <summary>
        /// The user's updated first name
        /// </summary>
        [Required(ErrorMessage = "First name is required")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// The user's updated last name
        /// </summary>
        [Required(ErrorMessage = "Last name is required")]
        public string LastName { get; set; } = string.Empty;
    }
}