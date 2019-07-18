using System.ComponentModel.DataAnnotations;

namespace Auth.API.Responses
{
    public class UserResponse
    {
        /// <summary>
        /// Username use to login
        /// </summary>
        /// <example>nguyenkim</example>
        public string username { get; set; }
        /// <summary>
        /// Email of user
        /// </summary>
        /// <example>example@gmail.com</example>
        [EmailAddress(ErrorMessage = "Not a valid email")]
        public string email { get; set; }

    }
}