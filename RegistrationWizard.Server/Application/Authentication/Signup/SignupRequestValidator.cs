using FluentValidation;

namespace RegistrationWizard.Server.Application.Authentication.Signup;

public class SignupRequestValidator : AbstractValidator<SingupRequest>
{
    public SignupRequestValidator()
    {
        RuleFor(x => x.Login)
            .NotNull()
            .EmailAddress();

        RuleFor(x => x.Password)
            .NotNull()
            .Must(x => x!.Any(char.IsDigit) && x!.Any(char.IsLetter));

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.Password);

        RuleFor(x => x.IsAgreeToWorkForFood)
            .Equal(true);
    }
}
