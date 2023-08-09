using CoreLibrary.Models;
using CoreLibrary.Repositories.Interfaces;

namespace CoreLibrary.Operations.Interfaces
{
    public interface IBookProcess : IBorrowableBookRepository<BorrowableBook, QueueBorrowBook>
    {
    }
}
