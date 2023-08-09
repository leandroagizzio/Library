using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class LoginModel
    {

        [Required(ErrorMessage = "Login is required")]
        public string Login { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
