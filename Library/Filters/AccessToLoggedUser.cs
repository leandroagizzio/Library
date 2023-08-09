using Library.Helper.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Library.Filters
{
    public class AccessToLoggedUser : ActionFilterAttribute
    {
        //cannot use dependency injection for filter, as service needed,
        //it can be reached through context.HttpContext.RequestServices.GetService<T>()
        public override void OnActionExecuting(ActionExecutingContext context) {

            var user = context.HttpContext.RequestServices.GetService<IUserSession>().GetUserSession();

            if (user == null) {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary {
                    { "controller", "Login" }, { "action", "Index" }
                });
            }

            base.OnActionExecuting(context);
        }
    }
}
