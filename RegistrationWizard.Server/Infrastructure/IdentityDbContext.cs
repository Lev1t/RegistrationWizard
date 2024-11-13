using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Server.Application.Common;
using RegistrationWizard.Server.Domain.Models;
using System.Reflection;

namespace RegistrationWizard.Server.Infrastructure;

public class IdentityDbContext : DbContext, IIdentityDbContext
{
    public DbSet<UserLogin> UserLogins { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Country> Countries { get; set; }
    public DbSet<Province> Provinces { get; set; }
    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
