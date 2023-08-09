using CoreLibrary.Models.Interfaces;
using Library.Models.Interfaces;

namespace Library.Models
{
    public class UserBookPossessionListViewModel : IUserBookPossessionListViewModel
    {
        public IList<IBook> Books { get; set; }
        public IUser User { get; set; }
    }
}
