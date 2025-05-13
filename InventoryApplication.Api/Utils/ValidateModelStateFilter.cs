using InventoryApplication.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InventoryApplication.Api.Utils
{
    public class ValidateModelStateFilter : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(!context.ModelState.IsValid)
            {
                context.Result = new BadRequestObjectResult(InvalidResult.Create(context.ModelState));
            }
        }
    }
}
