using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistrationWizard.Server.Domain.Models;

namespace RegistrationWizard.Server.Infrastructure.Configurations;

public class UserLoginConfig : IEntityTypeConfiguration<UserLogin>
{
    public void Configure(EntityTypeBuilder<UserLogin> builder)
    {
        builder.Property(x => x.Login)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.Login)
            .IsUnique();

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PasswordSalt)
            .IsRequired()
            .HasMaxLength(100);
    }
}
