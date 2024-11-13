using Microsoft.AspNetCore.Mvc;
using RegistrationWizard.Server.Application;
using RegistrationWizard.Server.Application.Authentication.Signup;
using RegistrationWizard.Server.Application.Common;

namespace RegistrationWizard.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IHandler<SingupRequest, HandlerResult> _signupRequestHandler;

    public AuthenticationController(
        IHandler<SingupRequest, HandlerResult> handler)
    {
        _signupRequestHandler = handler;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> Signup(SingupRequest singupRequest, CancellationToken ct)
    {
        var result = await _signupRequestHandler.Handle(singupRequest, ct);

        return result.ResultType switch
        {
            HandlerResultType.Success => Created(),
            HandlerResultType.Conflict => Conflict(result.ErrorMessage),
            HandlerResultType.InvalidRequest => BadRequest(result.ErrorMessage),
            _ => throw new NotSupportedException("Unexpected ResultType.")
        };
    }
}
