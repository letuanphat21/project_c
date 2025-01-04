using Doan.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Doan.Filters
{
    public class AdminAuthorizeAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var sessionUser = context.HttpContext.Session.GetString("user");
            if (string.IsNullOrEmpty(sessionUser))
            {
                context.Result = new RedirectToActionResult("Login", "Home", null);
                return;
            }

            var user = System.Text.Json.JsonSerializer.Deserialize<User>(sessionUser);

            if (user == null || !user.IsAdmin)
            {
                context.Result = new ForbidResult();
            }

            base.OnActionExecuting(context);
        }
    }
}
