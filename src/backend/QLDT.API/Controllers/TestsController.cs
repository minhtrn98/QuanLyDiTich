using Microsoft.AspNetCore.Mvc;
using QLDT.API.Common;
using QLDT.Application.Users.Commands;

namespace QLDT.API.Controllers;

[Route("api/tests")]
public sealed class TestsController : AppApiController
{
    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken cancellationToken)
    {
        Result<Success> result = await Mediator.Send(
            new CreateUser.Command("Super", "Admin", "123456", "admin@gmail.com"), cancellationToken);
        return OkOrProblem(result);
    }
}
