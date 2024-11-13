using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Server.Domain.Models;

namespace RegistrationWizard.Server.Application.Common;

public interface IIdentityDbContext
{
    public DbSet<UserLogin> UserLogins { get; }
    public DbSet<User> Users { get; }
    public DbSet<Country> Countries { get; }
    public DbSet<Province> Provinces { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
