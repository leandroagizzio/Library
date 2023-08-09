using CoreLibrary.Models.Interfaces;
using Library.Models.BookActionViewModel.Interfaces;
using Library.Models.Interfaces;

namespace Library.Models
{
    public class BooksUserViewModel : IBooksUserViewModel
    {
        public IList<IBookActionInfo> Books { get; set; }
        public IUser User { get; set; }
    }

}
