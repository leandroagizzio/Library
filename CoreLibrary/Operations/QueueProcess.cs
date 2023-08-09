using CoreLibrary.Data;
using CoreLibrary.Models;
using CoreLibrary.Operations.Interfaces;
using CoreLibrary.Repositories;

namespace CoreLibrary.Operations
{
    public class QueueProcess : QueueBorrowBookRepository<QueueBorrowBook>, IQueueProcess
    {
        public QueueProcess(Context context) : base(context, context.QueueBorrowBooks) {
        }
    }
}
