using Library.Models.BookActionViewModel.Interfaces;

namespace Library.Models.BookActionViewModel
{
    public class ActionCanRemoveFromQueue : IActionInfo
    {
        public string GetAction { get; set; } = "RemoveFromBookQueue";
        public string GetText { get; set; } = "Leave Book Queue";
        public string GetBtn { get; set; } = "btn-secondary";
    }
}
