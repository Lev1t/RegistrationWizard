using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationWizard.Server.Domain.Models;

namespace RegistrationWizard.Server.Infrastructure.Configurations;

public class CountryConfig : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(100);


        builder.HasData(
            new Country { Id = 1, Name = "Australia" },
            new Country { Id = 2, Name = "Brasilia", },
            new Country { Id = 3, Name = "Mexico" });
    }
}
