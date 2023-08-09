using Library.Models.BookActionViewModel.Interfaces;

namespace Library.Models.BookActionViewModel
{
    public class ActionCanGoToQueue : IActionInfo
    {
        public string GetAction { get; set; } = "GoToBookQueue";
        public string GetText { get; set; } = "Join Book Queue";
        public string GetBtn { get; set; } = "btn-primary";
    }
}
