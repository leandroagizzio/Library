using CoreLibrary.Data;
using CoreLibrary.Models;
using CoreLibrary.Operations;
using CoreLibrary.Operations.Interfaces;
using CoreLibrary.Repositories;
using CoreLibrary.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TestCore
{
    [TestClass]
    public class BorrowAndQueueTests
    {
        private readonly Context _dbContext;
        private readonly IBookProcess _bookAction;
        private readonly IQueueProcess _queueAction;
        private readonly IUserRepository _userRepository;
        private readonly IBookRepository _bookRepository;

        private readonly string _userA_Name = "UserA";
        private readonly string _userB_Name = "UserB";
        private readonly string _userC_Name = "UserC";

        private readonly string _bookA_Title = "BookA";
        private readonly string _bookB_Title = "BookB";

        private Book _BookA, _BookB;
        private User _UserA, _UserB, _UserC;

        public BorrowAndQueueTests() {
            _dbContext = new Context(
                (
                    (new DbContextOptionsBuilder<Context>())
                        .UseSqlite(connectionString: "DataSource=../../../../Library/Library.db;Cache=Shared")
                ).Options
                );

            _userRepository = new UserRepository(_dbContext);
            _bookRepository = new BookRepository(_dbContext);
            _queueAction = new QueueProcess(_dbContext);
            _bookAction = new BookProcess(_dbContext, _queueAction);

        }

        [TestMethod]
        public void Test01_CreateUserAANotAdmin() {
            var nameUser = "UserAA";
            var user = new User { Name = nameUser, IsAdmin = false, Email = "abc@abc.com", Login = nameUser, Password = nameUser };

            var userDB = _userRepository.Create(user);

            Assert.IsNotNull(userDB);
            Assert.AreEqual(nameUser, userDB.Name);
            Assert.IsFalse(userDB.IsAdmin);
        }

        [TestMethod]
        public void Test02_ModifyUserAA() {
            var nameUser = "UserAA";

            var userDB = _userRepository.ReadAll().FirstOrDefault(u => u.Name == nameUser);


            Assert.IsNotNull(userDB);

            userDB.Name = _userA_Name;
            userDB.IsAdmin = true;

            var newUserDB = _userRepository.Update(userDB);

            Assert.IsNotNull(newUserDB);

            Assert.AreEqual(_userA_Name, newUserDB.Name);
            Assert.IsTrue(newUserDB.IsAdmin);
        }

        [TestMethod]
        public void Test03_CreateModifyBookA() {
            var bookAA = "BookAA";
            var book = new Book { Name = bookAA, Author = "", DaysToBorrow = 7 };

            var bookDB = _bookRepository.Create(book);

            Assert.IsNotNull(bookDB);

            bookDB.Name = _bookA_Title;

            var newBookDB = _bookRepository.Update(bookDB);

            Assert.IsNotNull(newBookDB);

            Assert.AreEqual(_bookA_Title, newBookDB.Name);
        }

        [TestMethod]
        public void Test04_CreateOtherBooksUsers() {
            var bookB = new Book { Name = _bookB_Title, Author = "", DaysToBorrow = 7 };
            var userB = new User { Name = _userB_Name, IsAdmin = false, Email = "abc@abc.com", Login = _userB_Name, Password = _userB_Name };
            var userC = new User { Name = _userC_Name, IsAdmin = false, Email = "abc@abc.com", Login = _userC_Name, Password = _userC_Name };

            Assert.IsNotNull(_bookRepository.Create(bookB));
            Assert.IsNotNull(_userRepository.Create(userB));
            Assert.IsNotNull(_userRepository.Create(userC));
        }

        [TestMethod]
        public void Test05_BorrowActions() {
            GetBooksUsers();

            Assert.IsTrue(_bookAction.BorrowBook(_BookA, _UserA)); //first borrow
            Assert.IsFalse(_bookAction.BorrowBook(_BookA, _UserB)); //cannot borrow, someone has book

            Assert.IsTrue(_bookAction.SomeoneHasBook(_BookA)); //someone has book
            Assert.IsFalse(_bookAction.SomeoneHasBook(_BookB)); //no one has book

            Assert.IsFalse(_bookAction.ReturnBook(_BookB, _UserA)); //cannot return, no one has book
            Assert.IsFalse(_bookAction.ReturnBook(_BookA, _UserB)); //cannot return, dont have book
        }

        [TestMethod]
        public void Test06_QueueActions() {
            GetBooksUsers();

            Assert.IsTrue(_queueAction.AddToQueue(_BookA, _UserB)); //entering in queue
            Assert.IsTrue(_queueAction.AddToQueue(_BookA, _UserC)); //entering in queue
            Assert.IsFalse(_queueAction.AddToQueue(_BookA, _UserC)); //already in queue

            Assert.IsFalse(_queueAction.IsUserInQueue(_BookA, _UserA)); // A not in queue
            Assert.IsTrue(_queueAction.IsUserInQueue(_BookA, _UserB)); // B in queue
            Assert.IsTrue(_queueAction.IsUserInQueue(_BookA, _UserC)); // C in queue

            Assert.IsTrue(_queueAction.IsUserNextInQueue(_BookA, _UserB)); // B is next
            Assert.IsFalse(_queueAction.IsUserNextInQueue(_BookA, _UserC)); // C is not next

            Assert.IsTrue(_bookAction.ReturnBook(_BookA, _UserA)); //returning book

            Assert.IsFalse(_bookAction.BorrowBook(_BookA, _UserC)); //cannot borrow, not next in queue

            Assert.IsTrue(_bookAction.BorrowBook(_BookA, _UserA)); //can borrow, admin overrides
            Assert.IsTrue(_bookAction.ReturnBook(_BookA, _UserA)); //returning book

            Assert.IsTrue(_bookAction.BorrowBook(_BookA, _UserB)); //can borrow, next in queue
            Assert.IsFalse(_queueAction.IsUserInQueue(_BookA, _UserB)); //not in queue anymore as borrowed the book
            Assert.IsTrue(_bookAction.ReturnBook(_BookA, _UserB)); // return book

            Assert.IsFalse(_queueAction.IsQueueEmpty(_BookA)); //not empty, has C
            Assert.IsTrue(_bookAction.BorrowBook(_BookA, _UserC)); //can borrow, next in queue
            Assert.IsTrue(_queueAction.IsQueueEmpty(_BookA)); //now queue is empty
            Assert.IsTrue(_bookAction.ReturnBook(_BookA, _UserC)); // return book

        }


        [TestMethod]
        public void Test99_DeleteBooksUsers() {
            GetBooksUsers();

            Assert.IsNotNull(_UserA);
            Assert.IsNotNull(_UserB);
            Assert.IsNotNull(_UserC);

            Assert.IsNotNull(_BookA);
            Assert.IsNotNull(_BookB);

            Assert.IsTrue(_userRepository.Delete(_UserA.Id));
            Assert.IsTrue(_userRepository.Delete(_UserB.Id));
            Assert.IsTrue(_userRepository.Delete(_UserC.Id));

            Assert.IsTrue(_bookRepository.Delete(_BookA.Id));
            Assert.IsTrue(_bookRepository.Delete(_BookB.Id));
        }

        private void GetBooksUsers() {
            _UserA = _userRepository.ReadAll().FirstOrDefault(u => u.Name == _userA_Name);
            _UserB = _userRepository.ReadAll().FirstOrDefault(u => u.Name == _userB_Name);
            _UserC = _userRepository.ReadAll().FirstOrDefault(u => u.Name == _userC_Name);

            _BookA = _bookRepository.ReadAll().FirstOrDefault(b => b.Name == _bookA_Title);
            _BookB = _bookRepository.ReadAll().FirstOrDefault(b => b.Name == _bookB_Title);
        }
    }
}