using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Shop.Application.Common.Models;

namespace Shop.Presentation.Tools;

public class ValidationModelStateAttribute : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        if (!context.ModelState.IsValid)
        {
            var modelStateEntries = context.ModelState.Values;

            foreach (var entry in modelStateEntries)
            {
                foreach (var error in entry.Errors)
                {
                    ApiResult<string> resultObject = new(error.ErrorMessage);
                    context.Result = new JsonResult(resultObject);
                }
            }
        }
    }
}