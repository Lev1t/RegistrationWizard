namespace RegistrationWizard.Server.Application.Common.Services;

public interface IPasswordService
{
    PasswordHashSalt GetPasswordHashAndSalt(string password);
}
