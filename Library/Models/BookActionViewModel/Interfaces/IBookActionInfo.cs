using CoreLibrary.Models.Interfaces;

namespace Library.Models.BookActionViewModel.Interfaces
{
    public interface IBookActionInfo
    {
        IBook Book { get; set; }
        IActionInfo ActionInfo { get; set; }
    }
}
