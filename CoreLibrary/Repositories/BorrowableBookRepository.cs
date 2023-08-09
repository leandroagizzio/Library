using CoreLibrary.Data;
using CoreLibrary.Models;
using CoreLibrary.Models.Interfaces;
using CoreLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CoreLibrary.Repositories
{
    public class BorrowableBookRepository<T, U> : IBorrowableBookRepository<T, U>
       where T : class, IBorrowableBook, new()
       where U : class, IQueueBorrowBook
    {
        private readonly Context _context;
        private readonly DbSet<T> _dbSet;
        private readonly IQueueBorrowBookRepository<U> _queueBorrowBookRepository;
        public BorrowableBookRepository(Context context, DbSet<T> dbSet, IQueueBorrowBookRepository<U> queueBorrowBookRepository) {
            _context = context;
            _dbSet = dbSet;
            _queueBorrowBookRepository = queueBorrowBookRepository;
        }

        public T? GetBookStatus(IBook book) {
            //AsEnumerable to override client side query issue
            return _dbSet.AsEnumerable().FirstOrDefault(x => x.BookId == book.Id && x.IsInPossession());
            //return _dbSet.FirstOrDefault(x => x.BookId == book.Id && x.EndTime == null);
        }

        public IList<IBook> GetBooksInUserPossession(IUser user) {
            return _dbSet.Include(x => x.Book).AsEnumerable()
                .Where(x => x.UserId == user.Id && x.IsInPossession())
                .Select(x => x.Book).Cast<IBook>()
                .ToList();
        }

        public bool SomeoneHasBook(IBook book) {
            return GetBookStatus(book) != null;
        }

        public bool CanBorrow(IBook book, IUser user) {
            if (SomeoneHasBook(book))
                return false;
            if (!_queueBorrowBookRepository.CanBorrow(book, user))
                return false;
            return true;
        }

        public bool BorrowBook(IBook book, IUser user) {
            //if (SomeoneHasBook(book))
            //    return false;
            //if (!_queueBorrowBookRepository.CanBorrow(book, user))
            //    return false;

            if (!CanBorrow(book, user))
                return false;

            var borrowableBook = new T() {
                BookId = book.Id,
                UserId = user.Id,
                StartTime = DateTime.Now,
                EndTime = null
            };

            _context.Add(borrowableBook);
            if (_context.SaveChanges() <= 0)
                return false;

            _queueBorrowBookRepository.ExecuteBorrow(book, user);
            return true;
        }

        public bool ReturnBook(IBook book, IUser user) {
            //if (!SomeoneHasBook(book))
            //    return false;
            //var borrowableBook = GetBookStatus(book);
            //if (borrowableBook?.UserId != user.Id)
            //    return false;
            if (!CanReturn(book, user))
                return false;

            var borrowableBook = GetBookStatus(book);
            borrowableBook.EndTime = DateTime.Now;

            _context.Update(borrowableBook);
            if (_context.SaveChanges() <= 0)
                return false;

            return true;
        }

        public bool CanReturn(IBook book, IUser user) {
            if (!SomeoneHasBook(book))
                return false;
            var borrowableBook = GetBookStatus(book);
            if (borrowableBook?.UserId == user.Id)
                return true;
            return false;
        }

    }
}
