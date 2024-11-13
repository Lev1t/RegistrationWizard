using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Server.Application.Common;
using RegistrationWizard.Server.Application.Common.Services;
using RegistrationWizard.Server.Domain.Models;
using RegistrationWizard.Server.Infrastructure;

namespace RegistrationWizard.Server.Application.Authentication.Signup;

public class SingupRequestHandler : IHandler<SingupRequest, HandlerResult>
{
    private readonly IdentityDbContext _context;
    private readonly IPasswordService _passwordService;
    private readonly SignupRequestValidator _signupRequestValidator;

    public SingupRequestHandler(
        IdentityDbContext userContext,
        IPasswordService passwordService,
        SignupRequestValidator signupRequestValidator)
    {
        _context = userContext;
        _passwordService = passwordService;
        _signupRequestValidator = signupRequestValidator;
    }

    public async Task<HandlerResult> Handle(SingupRequest singupRequest, CancellationToken ct = default)
    {
        var validationResult = _signupRequestValidator.Validate(singupRequest);
        if (!validationResult.IsValid)
        {
            var errorMessage = string.Join($";{Environment.NewLine}",
                validationResult.Errors.Select(x => x.ErrorMessage));
            return HandlerResult.Invalid(errorMessage);
        }

        var isLoginExist = await _context.UserLogins
            .AnyAsync(x => x.Login == singupRequest.Login, ct);

        if (isLoginExist)
        {
            return HandlerResult.Conflict("Login is already taken.");
        }

        var hashSalt = _passwordService.GetPasswordHashAndSalt(singupRequest.Password!);

        //mappper on demand
        var user = new User
        {
            UserLogin = new UserLogin
            {
                Login = singupRequest.Login!,
                PasswordHash = hashSalt.PasswordHash,
                PasswordSalt = hashSalt.Salt
            },
            CountryId = singupRequest.CountryId,
            ProvinceId = singupRequest.ProvinceId,
            IsAgreeToWorkForFood = singupRequest.IsAgreeToWorkForFood
        };

        _context.Add(user);
        await _context.SaveChangesAsync(ct);

        return HandlerResult.Success();
    }
}
