namespace RegistrationWizard.Server.Domain.Models;

public class User
{
    public int Id { get; set; }
    public bool IsAgreeToWorkForFood { get; set; }

    public int CountryId { get; set; }
    public Country Country { get; set; } = null!;

    public int ProvinceId { get; set; }
    public Province Province { get; set; } = null!;

    public UserLogin UserLogin { get; set; } = null!;
}
