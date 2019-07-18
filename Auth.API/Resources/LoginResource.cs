using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Auth.API.Resources
{
    public class LoginResource
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
    }
}