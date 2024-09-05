using Common.Helper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Models.Responses;

namespace din_im_liau.Middlewares;

public class ErrorMessageFilter : ActionFilterAttribute, IActionFilter
{
    public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorObj = context.ModelState.Select(p =>
            new
            {
                Field = RegexHelper.ConvertToSnakeCase(p.Key),
                Message = string.Join('\n', p.Value?.Errors.Select(e => e.ErrorMessage) ?? [])
            });

            // var errors = context.ModelState.Values
            //             .Where(x => x.Errors.Count > 0)
            //             .SelectMany(x => x.Errors)
            //             .Select(x => x.ErrorMessage);
            // var errorsString = string.Join('\n', errors);
            var responseObj = new GenericResponse<object> { Data = errorObj };

            var badresult = new BadRequestObjectResult(responseObj);
            context.Result = badresult;

            return;


        }

        await next();
    }

    public override async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (!context.ModelState.IsValid)
        {
            var errorObj = context.ModelState.Select(p =>
            new
            {
                Field = RegexHelper.ConvertToSnakeCase(p.Key),
                Message = string.Join('\n', p.Value?.Errors.Select(e => e.ErrorMessage) ?? [])
            });

            // var errors = context.ModelState.Values
            //             .Where(x => x.Errors.Count > 0)
            //             .SelectMany(x => x.Errors)
            //             .Select(x => x.ErrorMessage);
            // var errorsString = string.Join('\n', errors);

            var responseObj = new DefaultResponse { Data = errorObj };

            var badresult = new BadRequestObjectResult(responseObj);

            context.Result = badresult;



        }
        await next();
    }

}
