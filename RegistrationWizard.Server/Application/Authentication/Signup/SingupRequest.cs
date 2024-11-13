namespace RegistrationWizard.Server.Application.Authentication.Signup;

public class SingupRequest
{
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? ConfirmPassword { get; set; }
    public bool IsAgreeToWorkForFood { get; set; }
    public int CountryId { get; set; }
    public int ProvinceId { get; set; }
}
