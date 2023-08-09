using CoreLibrary.Repositories.Interfaces;
using Library.Helper;
using Library.Helper.Interfaces;
using Library.Models;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Library.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserSession _userSession;

        public LoginController(IUserRepository userRepository, IUserSession userSession)
        {
            _userRepository = userRepository;
            _userSession = userSession;
        }
        public IActionResult Index() {
            if (_userSession.GetUserSession() != null) 
                return RedirectToAction("Index", "Home");            
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel loginModel) {
            try {
                if (ModelState.IsValid) {
                    var user = _userRepository.ReadByLogin(loginModel.Login);

                    if (user is not null && user.ValidatePassword(loginModel.Password)) {
                        _userSession.CreateUserSession(user);
                        return GoToHomeIndex();
                    }

                    TempData[HelperData.GetErrorTempDataKey] = "User and/or password invalid";
                }
                return View("Index", loginModel);
            } catch (Exception error) {
                TempData[HelperData.GetErrorTempDataKey] = $"Error generated: {error.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Logout() {
            _userSession.RemoveUserSession();
            return RedirectToAction("Index", "Login");
        }

        private IActionResult GoToHomeIndex() => RedirectToAction("Index", "Home");
    }
}
