using CoreLibrary.Models;
using Library.Helper;
using Library.Helper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Library.ViewComponents
{
    public class Menu : ViewComponent
    {
        private readonly IUserSession _userSession;

        public Menu(IUserSession userSession)
        {
            _userSession = userSession;
        }
        public async Task<IViewComponentResult> InvokeAsync() {
            //string userSession = HttpContext.Session.GetString(HelperData.GetUserSessionKey);

            //if (string.IsNullOrEmpty(userSession))
            //    return null;

            //var user = JsonConvert.DeserializeObject<User>(userSession);

            //return View(user);
            return View(_userSession.GetUserSession());
        }
    }
}
