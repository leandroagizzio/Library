using CoreLibrary.Data;
using CoreLibrary.Models;
using CoreLibrary.Operations.Interfaces;
using CoreLibrary.Repositories;

namespace CoreLibrary.Operations
{
    public class BookProcess : BorrowableBookRepository<BorrowableBook, QueueBorrowBook>, IBookProcess
    {
        public BookProcess(Context context, IQueueProcess queueAction)
            : base(context, context.BorrowableBooks, queueAction) {
        }
    }
}
