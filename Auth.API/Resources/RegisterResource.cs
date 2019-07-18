using System.ComponentModel.DataAnnotations;

namespace Auth.API.Resources
{
    public class RegisterResource
    {
        /// <summary>
        /// Username use to login
        /// </summary>
        /// <example>nguyenkim</example>
        [Required]
        public string username { get; set; }
        /// <summary>
        /// Password use to authenticate user
        /// </summary>
        /// <example>123456789</example>
        [Required]
        [MinLength(8)]
        public string password { get; set; }
        /// <summary>
        /// Email of user
        /// </summary>
        /// <example>Ho Chi Minh</example>
        [Required]
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string email { get; set; }

    }
}