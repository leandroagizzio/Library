using CoreLibrary.Models.Interfaces;
using Library.Models.BookActionViewModel.Interfaces;

namespace Library.Models.Interfaces
{
    public interface IBooksUserViewModel
    {
        IList<IBookActionInfo> Books { get; set; }
        IUser User { get; set; }
    }
}
