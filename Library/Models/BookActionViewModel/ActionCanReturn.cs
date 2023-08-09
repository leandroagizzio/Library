using Library.Models.BookActionViewModel.Interfaces;

namespace Library.Models.BookActionViewModel
{
    public class ActionCanReturn : IActionInfo
    {
        public string GetAction { get; set; } = "ReturnBook";
        public string GetText { get; set; } = "Return the Book";
        public string GetBtn { get; set; } = "btn-danger";
    }
}
