using FluentValidation;
using Microsoft.EntityFrameworkCore;
using RegistrationWizard.Server.Application;
using RegistrationWizard.Server.Application.Authentication.Signup;
using RegistrationWizard.Server.Application.Common;
using RegistrationWizard.Server.Application.Common.Services;
using RegistrationWizard.Server.Infrastructure;
using System.Reflection;


//TODO:
//Create projects for each layer
//Use endpoints instead of controllers

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Host.ConfigureServices(ConfigureServices);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.Services.CreateScope().ServiceProvider
        .GetRequiredService<IdentityDbContext>().Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors(x => x.AllowAnyHeader()
    .AllowAnyMethod()
    .SetIsOriginAllowed(x => true));

app.UseAuthorization();


app.MapControllers();

app.Run();

static void ConfigureServices(HostBuilderContext context, IServiceCollection services)
{
    //separate extension for request handlers
    //wanted to replace mediatr with custom solution for navigation simplicity
    Assembly.GetExecutingAssembly()
        .GetTypes()
        .Where(x => x.GetInterfaces()
            .Any(i =>
                i.IsGenericType
                && i.GetGenericTypeDefinition() == typeof(IHandler<,>))
                && !x.IsAbstract
                && !x.IsInterface)
        .Select(x => new
        {
            assignedType = x,
            handlerInterface = x.GetInterfaces()
                    .First(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IHandler<,>))
        })
        .ToList()
        .ForEach(typesToRegister =>
        {
            services.AddTransient(typesToRegister.handlerInterface, typesToRegister.assignedType);
        });


    //separate extension for shared services
    services.AddTransient<IPasswordService, PasswordService>();

    //separate extension for contexts
    services.AddDbContext<IIdentityDbContext, IdentityDbContext>(opt =>
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        var filename = context.Configuration.GetValue<string>("SQLiteDbFileName");
        var dbPath = Path.Join(path, filename);
        opt.UseSqlite($"Data Source={dbPath}");
    });

    //validators
    services.AddValidatorsFromAssemblyContaining<SignupRequestValidator>();
}
