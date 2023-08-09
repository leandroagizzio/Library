using Library.Models.BookActionViewModel.Interfaces;

namespace Library.Models.BookActionViewModel
{
    public class ActionCanBorrow : IActionInfo
    {
        public string GetAction { get; set; } = "BorrowBook";
        public string GetText { get; set; } = "Borrow the Book";
        public string GetBtn { get; set; } = "btn-success";
    }
}
