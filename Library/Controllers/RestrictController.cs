using Library.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [AccessToLoggedUser]
    public class RestrictController : Controller
    {
        public IActionResult Index() {
            return View();
        }
    }
}
