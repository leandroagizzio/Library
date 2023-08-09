using CoreLibrary.Data;
using CoreLibrary.Models;
using CoreLibrary.Models.Interfaces;
using CoreLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Repositories
{
    public class QueueBorrowBookRepository<T> : IQueueBorrowBookRepository<T> where T : class, IQueueBorrowBook, new()
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;

        public QueueBorrowBookRepository(Context context, DbSet<T> dbSet) {
            _context = context;
            _dbSet = dbSet;
        }

        public IList<T> GetQueueBook(IBook book) {
            return _dbSet.Where(x => x.BookId == book.Id && x.IsFinished == false).OrderBy(x => x.Id).ToList();
        }

        public bool IsQueueEmpty(IBook book) {
            return GetQueueBook(book).Count == 0;
        }

        public int NextUserInLine(IBook book) {
            if (IsQueueEmpty(book))
                return 0;
            var nextUserId = GetQueueBook(book).First().UserId;
            return nextUserId;
        }

        public bool AddToQueue(IBook book, IUser user) {
            if (IsUserInQueue(book, user))
                return false;
            var queueBorrowBook = new T() {
                UserId = user.Id,
                BookId = book.Id,
                IsFinished = false
            };

            _context.Add(queueBorrowBook);
            if (_context.SaveChanges() <= 0)
                return false;

            return true;
        }

        public bool IsUserInQueue(IBook book, IUser user) {
            return GetQueueBook(book).FirstOrDefault(x => x.UserId == user.Id) != null ? true : false;
        }

        public bool IsUserNextInQueue(IBook book, IUser user) {
            if (IsQueueEmpty(book))
                return false;
            if (NextUserInLine(book) == user.Id)
                return true;
            return false;
        }

        public bool RemoveFromQueue(IBook book, IUser user) {
            if (!IsUserInQueue(book, user))
                return false;
            var userInQueue = GetQueueBook(book).FirstOrDefault(x => x.UserId == user.Id);
            userInQueue.IsFinished = true;

            _context.Update(userInQueue);
            if (_context.SaveChanges() <= 0)
                return false;

            return true;
        }

        public bool CanBorrow(IBook book, IUser user) {
            if (NextUserInLine(book) == user.Id)
                return true;
            if (IsQueueEmpty(book))
                return true;
            if (user.IsAdmin)
                return true;
            return false;
        }

        public bool ExecuteBorrow(IBook book, IUser user) {
            if (!CanBorrow(book, user))
                return false;
            RemoveFromQueue(book, user);
            return true;
        }

        public IList<int> GetBookIdsThatUserIsInQueue(IUser user) {
            return _dbSet.Where(x => x.UserId == user.Id && x.IsFinished == false).Select(x => x.BookId).ToList();
        }
    }
}
