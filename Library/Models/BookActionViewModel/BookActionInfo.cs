using CoreLibrary.Models;
using CoreLibrary.Models.Interfaces;
using Library.Models.BookActionViewModel.Interfaces;

namespace Library.Models.BookActionViewModel
{
    public class BookActionInfo : IBookActionInfo
    {
        public IBook Book { get; set; }
        public IActionInfo ActionInfo { get; set; }
    }
}
