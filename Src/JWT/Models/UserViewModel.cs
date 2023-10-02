using Services.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace JWT.Models
{
    public class UserViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
