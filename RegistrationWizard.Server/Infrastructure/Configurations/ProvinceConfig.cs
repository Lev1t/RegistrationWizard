using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationWizard.Server.Domain.Models;

namespace RegistrationWizard.Server.Infrastructure.Configurations;

public class ProvinceConfig : IEntityTypeConfiguration<Province>
{
    public void Configure(EntityTypeBuilder<Province> builder)
    {
        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasData(
            new Province { Id = 1, Name = "Province 1", CountryId = 1 },
            new Province { Id = 2, Name = "Province 2", CountryId = 1 },
            new Province { Id = 3, Name = "Province 3", CountryId = 2 },
            new Province { Id = 4, Name = "Province 4", CountryId = 2 },
            new Province { Id = 5, Name = "Province 5", CountryId = 3 },
            new Province { Id = 6, Name = "Province 6", CountryId = 3 });
    }
}
