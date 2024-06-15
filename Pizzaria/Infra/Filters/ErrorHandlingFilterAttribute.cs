using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

public class ErrorHandlingFilterAttribute : ExceptionFilterAttribute
{
    public override void OnException(ExceptionContext context)
    {        
        ProblemDetails problem;

        if (context.Exception is ApiException apiException)
        {
            problem = new ProblemDetails 
            {            
                Detail = apiException.Message,
                Status = apiException.StatusCode
            };

            context.Result = new ObjectResult(problem);

            context.ExceptionHandled = true;

            return;
        }

        problem = new ProblemDetails 
        {
            Detail = context.Exception.Message         
        };

        context.Result = new ObjectResult(problem);

        context.ExceptionHandled = true;
    }
}