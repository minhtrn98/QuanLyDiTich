using Microsoft.AspNetCore.Mvc;

namespace QLDT.API.Common;

[ApiController]
public abstract class AppApiController : ControllerBase
{
    private IMediator _mediator = default!;
    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>()!;

    protected IActionResult OkOrProblem<TValueOk>(Result<TValueOk> result)
    {
        return result switch
        {
            { IsSuccess: true, Value: Success } => Ok(),
            _ => result.Match(value => Ok(value), AppProblem)
        };
    }

    protected IActionResult NoContentOrProblem<TValueOk>(Result<TValueOk> result)
    {
        return result.Match(_ => NoContent(), AppProblem);
    }

    private ActionResult AppProblem(List<Error> errors)
    {
        // should never happen
        if (errors is [])
        {
            return Problem();
        }

        // support return one error at the time
        // except for validation error can return many
        if (errors[0].Type != ErrorType.Validation)
        {
            return HandledProblem(errors[0]);
        }

        return ValidationProblem(errors);
    }

    private ObjectResult HandledProblem(Error error)
    {
        return Problem(
            title: "An error occurred!",
            detail: error.Description,
            type: error.Code,
            statusCode: error.Type.ToStatusCode()
        );
    }

    private ObjectResult ValidationProblem(List<Error> errors)
    {
        ProblemDetails problemDetails = ProblemDetailsFactory.CreateProblemDetails(
            HttpContext,
            type: "Validation",
            title: "An error occurred!",
            detail: "One or more validation errors occurred.",
            statusCode: 400
        );
        problemDetails.Extensions["errors"] = ParseErrors(errors);

        return new ObjectResult(problemDetails);
    }

    private static Dictionary<string, object[]> ParseErrors(List<Error> errors)
    {
        Dictionary<string, object[]> errDic = [];
        errors
            .Where(err => err.PropertyName is not null)
            .GroupBy(err => err.PropertyName)
            .ToList()
            .ForEach(errGr => errDic.Add(errGr.Key!, errGr.Select(err => new { code = err.Code, err.Description }).ToArray()));
        return errDic;
    }
}
