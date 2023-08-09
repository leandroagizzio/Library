using CoreLibrary.Models.Interfaces;

namespace Library.Models.Interfaces
{
    public interface IUserBookPossessionListViewModel
    {
        IList<IBook> Books { get; set; }
        IUser User { get; set; }
    }
}
