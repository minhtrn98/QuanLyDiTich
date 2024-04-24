using System.Net;

namespace QLDT.API;

public class ExceptionToProblemDetailsHandler : Microsoft.AspNetCore.Diagnostics.IExceptionHandler
{
    private readonly IProblemDetailsService _problemDetailsService;

    public ExceptionToProblemDetailsHandler(IProblemDetailsService problemDetailsService)
    {
        _problemDetailsService = problemDetailsService;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
        return await _problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            ProblemDetails =
                {
                    Type = exception.GetType().Name,
                    Title = "An exception occurred!",
                    Detail = exception.Message,
                },
            Exception = exception
        });
    }
}
