using CoreLibrary.Models.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace CoreLibrary.Models
{
    public class Book : IBook
    {
        [Key()]
        public int Id { get; set; }
        [Required(ErrorMessage = "Book Name is required")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Book Author is required")]
        public string Author { get; set; }
        [Required(ErrorMessage = "Days to borrow is required")]
        [Range(1, 7, ErrorMessage = "Days should be between 1 to 7")]
        public int DaysToBorrow { get; set; }
    }
}
