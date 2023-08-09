using CoreLibrary.Models;
using CoreLibrary.Repositories.Interfaces;

namespace CoreLibrary.Operations.Interfaces
{
    public interface IQueueProcess : IQueueBorrowBookRepository<QueueBorrowBook>
    {
    }
}
