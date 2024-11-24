using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Redarbor.Candidates.Api.Domain.Exceptions;
using Serilog;

namespace Redarbor.Candidates.Api.Presentation.Filters;

public class CandidateExceptionFilter : ExceptionFilterAttribute
{
    private readonly Dictionary<Type, Action<ExceptionContext>> _exceptionHandlers = new()
    {
        { typeof(NotFoundCandidateException), HandleExceptionAsInvalidRequest }
    };

    public override void OnException(ExceptionContext context)
    {
        HandleException(context);
        base.OnException(context);
    }

    private void HandleException(ExceptionContext context)
    {
        HandleGenericException(context);
        var type = context.Exception.GetType();
        if (_exceptionHandlers.TryGetValue(type, out var handler))
        {
            handler.Invoke(context);
            context.ExceptionHandled = true;
        }

        Log.Error(context.Exception,
            $"StatusCode: {context.HttpContext.Response.StatusCode} Handled: {context.ExceptionHandled}");
    }

    private static void HandleGenericException(ExceptionContext context)
    {
        context.Result = new JsonResult(
            new { ErrorMessage = "Internal server error, try again.", ErrorCode = "500" });
        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    }

    private static void HandleExceptionAsInvalidRequest(ExceptionContext context)
    {
        var exception = context.Exception;
        context.Result = new JsonResult(
            new { ErrorMessage = exception.Message, ErrorCode = "400" });
        context.HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
    }
}