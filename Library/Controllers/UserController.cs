using CoreLibrary.Helper.ExtensionMethods;
using CoreLibrary.Models;
using CoreLibrary.Operations.Interfaces;
using CoreLibrary.Repositories.Interfaces;
using Library.Filters;
using Library.Helper;
using Library.Helper.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [AccessToLoggedUser]
    [AccessToAdminUser]
    public class UserController : BaseCrudController<User>
    {
        private readonly IUserRepository _userRepository;
        private readonly IBookProcess _bookProcess;

        public UserController(IUserRepository userRepository, IBookProcess bookProcess) : base(userRepository, "User")
        {
            _userRepository = userRepository;
            _bookProcess = bookProcess;
        }

        public override IActionResult Index() {
            var users = _userRepository.ReadAll();
            var list = HelperFactory.GetNewListUserBookPossessionViewModel();

            foreach (var user in users) {
                var booksPossessionViewModel = HelperFactory.GetNewUserBookPossessionListViewModel();
                booksPossessionViewModel.User = user;
                booksPossessionViewModel.Books = _bookProcess.GetBooksInUserPossession(user);
                list.Add(booksPossessionViewModel);
            }

            return View(list);
        }

        [HttpPost]
        public IActionResult EditWithoutPassword(UserWithoutPassword userWithoutPassword) {
            var user = HelperFactory.GetNewUserInstance();
            user.CopyToMe(userWithoutPassword);
            if (ModelState.IsValid)
                return base.Edit(user);
            return View("Edit", user);
        }

        public IActionResult GetBookListInUserPossession(int id) {
            var user = _userRepository.Read(id);
            var bookList = _bookProcess.GetBooksInUserPossession(user);

            return PartialView("_UserBookList", bookList);
        }

    }
}
