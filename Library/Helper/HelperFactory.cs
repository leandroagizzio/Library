using CoreLibrary.Models;
using Library.Models;
using Library.Models.BookActionViewModel;
using Library.Models.BookActionViewModel.Interfaces;
using Library.Models.Interfaces;

namespace Library.Helper
{
    public static class HelperFactory
    {
        public static User GetNewUserInstance() => new User();

        //BooksUserViewModel
        public static IBooksUserViewModel GetNewBooksUserViewModel() => new BooksUserViewModel();
        public static IList<IBookActionInfo> GetNewListIBookActionInfo() => new List<IBookActionInfo>();
        public static IBookActionInfo GetNewBookActionInfoInstance() => new BookActionInfo();
        public static IActionInfo GetNewActionCanBorrow() => new ActionCanBorrow();
        public static IActionInfo GetNewActionCanReturn() => new ActionCanReturn();
        public static IActionInfo GetNewActionCanGoToQueue() => new ActionCanGoToQueue();
        public static IActionInfo GetNewActionCanRemoveFromQueue() => new ActionCanRemoveFromQueue();

        //UserBookPossessionListViewModel
        public static IUserBookPossessionListViewModel GetNewUserBookPossessionListViewModel() => new UserBookPossessionListViewModel();
        public static IList<IUserBookPossessionListViewModel> GetNewListUserBookPossessionViewModel() => 
            new List<IUserBookPossessionListViewModel>();
    }
}
