using System.Security.Cryptography;

namespace RegistrationWizard.Server.Application.Common.Services;

public class PasswordService : IPasswordService
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100000;

    private static readonly HashAlgorithmName Algorithm = HashAlgorithmName.SHA256;

    public PasswordHashSalt GetPasswordHashAndSalt(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(password, salt, Iterations, Algorithm, HashSize);
        return new PasswordHashSalt
        {
            PasswordHash = Convert.ToHexString(hash),
            Salt = Convert.ToHexString(salt)
        };
    }
}
