using CoreLibrary.Models.Interfaces;

namespace CoreLibrary.Repositories.Interfaces
{
    public interface IBorrowableBookRepository<T, U>
        where T : class, IBorrowableBook
        where U : class, IQueueBorrowBook
    {
        bool CanBorrow(IBook book, IUser user);
        bool BorrowBook(IBook book, IUser user);
        T? GetBookStatus(IBook book);
        bool CanReturn(IBook book, IUser user);
        bool ReturnBook(IBook book, IUser user);
        bool SomeoneHasBook(IBook book);
        IList<IBook> GetBooksInUserPossession(IUser user);
    }
}
