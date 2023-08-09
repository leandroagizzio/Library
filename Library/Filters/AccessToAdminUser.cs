using Library.Helper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Library.Filters
{
    public class AccessToAdminUser : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context) {

            var user = context.HttpContext.RequestServices.GetService<IUserSession>().GetUserSession();

            if (user == null) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "controller", "Login" }, { "action", "Index" }
                });
            }

            if (!user.IsAdmin){
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "controller", "Restrict" }, { "action", "Index" }
                });
            }

            base.OnActionExecuting(context);
        }
    }
}
