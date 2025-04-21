using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace MVC_D8.CustomFilters
{
    public class MyExeptionHandler:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (context.Exception != null)
            {
                context.ExceptionHandled = true;
               context.Result = new ContentResult() { Content = "Custom error message by hoshy" };
            }
            base.OnActionExecuted(context);
        }
    }
}
