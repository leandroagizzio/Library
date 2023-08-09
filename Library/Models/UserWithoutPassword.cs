using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class UserWithoutPassword
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "User Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Pick one option")]
        public bool IsAdmin { get; set; }
        [Required(ErrorMessage = "User Email is required")]
        [EmailAddress(ErrorMessage = "Email is invalid")]
        public string Email { get; set; }

        [Required(ErrorMessage = "User Login is required")]
        public string Login { get; set; }
    }
}
