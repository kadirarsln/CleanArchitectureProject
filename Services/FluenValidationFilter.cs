using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace AppServices;

public class FluenValidationFilter : IAsyncActionFilter

{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errors = context.ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();

            var resultModel = ServiceResult.Failure(errors);
            context.Result = new BadRequestObjectResult(resultModel);
            return;
        }
        await next();

    }
}

