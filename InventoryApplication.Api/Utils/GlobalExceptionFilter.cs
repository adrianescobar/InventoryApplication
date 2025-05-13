using InventoryApplication.Api.Dtos;
using InventoryApplication.Services.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;

namespace InventoryApplication.Api.Utils
{
    public class GlobalExceptionFilter(ILogger<GlobalExceptionFilter> logger) : ExceptionFilterAttribute
    {
        public override void OnException(ExceptionContext context)
        {
            if(context.Exception is InventoryApplicationException)
            {
                logger.LogInformation(context.Exception, context.Exception.Message);
                context.Result = new BadRequestObjectResult(InvalidResult.Create(context.Exception.Message))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };
            }
            else if(context.Exception is Exception)
            {
                logger.LogError(context.Exception, Messages.GenericError);
                context.Result = new NotFoundObjectResult(InvalidResult.Create(Messages.GenericError))
                {
                    StatusCode = StatusCodes.Status500InternalServerError
                };
            }

            context.ExceptionHandled = true;
        }
    }
}
