using CoreLibrary.Helper.ExtensionMethods;
using CoreLibrary.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CoreLibrary.Models
{
    public class User : IUser 
    {
        [Key()]
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

        [Required(ErrorMessage = "User Password is required")]
        public string Password { get; set; }

        public bool ValidatePassword(string password) {
            return Password == password.GetHash();
        }

        public void SetHashPassword() {
            Password = Password.GetHash();
        }
    }
}
