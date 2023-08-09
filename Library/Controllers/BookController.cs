using CoreLibrary.Models;
using CoreLibrary.Models.Interfaces;
using CoreLibrary.Operations;
using CoreLibrary.Operations.Interfaces;
using CoreLibrary.Repositories.Interfaces;
using Library.Filters;
using Library.Helper;
using Library.Helper.Interfaces;
using Library.Models;
using Library.Models.BookActionViewModel;
using Library.Models.BookActionViewModel.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [AccessToLoggedUser]
    public class BookController : BaseCrudController<Book>
    {
        private readonly IBookRepository _bookRepository;
        private readonly IUserSession _userSession;
        private readonly IBookProcess _bookProcess;
        private readonly IQueueProcess _queueProcess;

        public BookController(IBookRepository bookRepository, IUserSession userSession, 
                                IBookProcess bookProcess, IQueueProcess queueProcess) : base(bookRepository, "Book") {
            _bookRepository = bookRepository;
            _userSession = userSession;
            _bookProcess = bookProcess;
            _queueProcess = queueProcess;
        }

        public override IActionResult Index() {            
            var user = _userSession.GetUserSession();
            var books = _bookRepository.ReadAll();
            var bookActionInfos = HelperFactory.GetNewListIBookActionInfo();

            foreach (var book in books) {
                var bookActionInfo = HelperFactory.GetNewBookActionInfoInstance();
                bookActionInfo.Book = book;
                bookActionInfo.ActionInfo = GetActionInfoForBookUser(book, user);
                bookActionInfos.Add(bookActionInfo);
            }

            var booksUserViewModel = HelperFactory.GetNewBooksUserViewModel();
            booksUserViewModel.Books = bookActionInfos;
            booksUserViewModel.User = user;

            return View(booksUserViewModel);
        }

        private IActionInfo GetActionInfoForBookUser(IBook book, IUser user) {
            if (_bookProcess.CanBorrow(book, user))
                return HelperFactory.GetNewActionCanBorrow();
            if (_bookProcess.CanReturn(book, user))
                return HelperFactory.GetNewActionCanReturn();
            if (_queueProcess.IsUserInQueue(book, user))
                return HelperFactory.GetNewActionCanRemoveFromQueue();
            return HelperFactory.GetNewActionCanGoToQueue();
        }

        [AccessToAdminUser]
        public override IActionResult Create() {
            return base.Create();
        }

        [AccessToAdminUser]
        public override IActionResult Create(Book model) {
            return base.Create(model);
        }

        [AccessToAdminUser]
        public override IActionResult Edit(int id) {
            return base.Edit(id);
        }

        [AccessToAdminUser]
        public override IActionResult Edit(Book model) {
            return base.Edit(model);
        }

        [AccessToAdminUser]
        public override IActionResult DeleteConfirmation(int id) {
            return base.DeleteConfirmation(id);
        }

        [AccessToAdminUser]
        public override IActionResult Delete(int id) {
            return base.Delete(id);
        }

        public IActionResult BorrowBook(int bookId) {
            return ExecuteProcessWrapper(
                bookId, 
                _bookProcess.BorrowBook, 
                "Book borrowed with success", 
                "Error in borrowing the book"
            );
        }

        public IActionResult ReturnBook(int bookId) {
            return ExecuteProcessWrapper(
                bookId,
                _bookProcess.ReturnBook,
                "Book returned with success",
                "Error in returning the book"
            );
        }
        public IActionResult GoToBookQueue(int bookId) {
            return ExecuteProcessWrapper(
                bookId,
                _queueProcess.AddToQueue,
                "Joined Book queue with success",
                "Error in joining the Book queue"
            );
        }
        public IActionResult RemoveFromBookQueue(int bookId) {
            return ExecuteProcessWrapper(
                bookId,
                _queueProcess.RemoveFromQueue,
                "Removed from Book queue with success",
                "Error in removing from Book queue"
            );
        }

        private IActionResult ExecuteProcessWrapper(int bookId, Func<IBook, IUser, bool> funcProcess, string success, string error) {
            if (ExecuteProcess(bookId, funcProcess))
                TempData[HelperData.GetSuccessTempDataKey] = success;
            else
                TempData[HelperData.GetErrorTempDataKey] = error;

            return GoToIndex();
        }

        private bool ExecuteProcess(int bookId, Func<IBook, IUser, bool> funcProcess) {
            var user = _userSession.GetUserSession();
            if (user == null)
                return false;
            var book = _bookRepository.Read(bookId);
            if (book == null)
                return false;     
            return funcProcess(book, user);
        }

    }
}
