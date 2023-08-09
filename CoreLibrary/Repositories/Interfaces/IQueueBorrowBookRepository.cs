using CoreLibrary.Models.Interfaces;

namespace CoreLibrary.Repositories.Interfaces
{
    public interface IQueueBorrowBookRepository<T> where T : class, IQueueBorrowBook
    {
        bool AddToQueue(IBook book, IUser user);
        bool CanBorrow(IBook book, IUser user);
        bool ExecuteBorrow(IBook book, IUser user);
        IList<T> GetQueueBook(IBook book);
        bool IsQueueEmpty(IBook book);
        bool IsUserInQueue(IBook book, IUser user);
        int NextUserInLine(IBook book);
        bool IsUserNextInQueue(IBook book, IUser user);
        bool RemoveFromQueue(IBook book, IUser user);
        IList<int> GetBookIdsThatUserIsInQueue(IUser user);
    }
}
