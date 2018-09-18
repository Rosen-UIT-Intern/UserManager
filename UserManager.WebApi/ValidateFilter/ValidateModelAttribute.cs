using System.Linq;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

using Newtonsoft.Json;

namespace UserManager.WebApi.ValidateFilter
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                //list all error message
                var list = from modelState in context.ModelState.Values
                           from error in modelState.Errors
                           select error.ErrorMessage
                           ;
                context.Result = new BadRequestObjectResult(JsonConvert.SerializeObject(list));
            }

            base.OnActionExecuting(context);
        }
    }
}
