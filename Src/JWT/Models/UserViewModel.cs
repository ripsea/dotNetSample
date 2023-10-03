using Services.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace JWT.Models
{
    public class UserViewModel
    {
        /// <summary>
        ///     帳號
        /// </summary>
        /// <example>iris</example>
        [Required]
        [StringLength(32)]
        public string Name { get; set; }
        /// <summary>
        ///     密碼
        /// </summary>
        /// <example>thisispassword</example>
        [Required]
        [StringLength(32)]
        public string Password { get; set; }
    }
}
