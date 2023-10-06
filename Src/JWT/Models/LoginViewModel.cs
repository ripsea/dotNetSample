using System.ComponentModel.DataAnnotations;

namespace Jwt.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "User Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
