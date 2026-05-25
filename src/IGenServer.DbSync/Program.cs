using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Domain.Entities;
using IGenServer.Domain.Enums;
using IGenServer.Infrastructure;
using IGenServer.Persistance.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.IO;

var builder = Host.CreateApplicationBuilder(args);
var environmentName = builder.Environment.EnvironmentName;
var projectDirectory = Path.Combine(Directory.GetCurrentDirectory(), "src", "IGenServer.DbSync");

builder.Configuration
    .AddJsonFile(Path.Combine(AppContext.BaseDirectory, "appsettings.json"), optional: true, reloadOnChange: false)
    .AddJsonFile(
        Path.Combine(AppContext.BaseDirectory, $"appsettings.{environmentName}.json"),
        optional: true,
        reloadOnChange: false)
    .AddJsonFile(Path.Combine(projectDirectory, "appsettings.json"), optional: true, reloadOnChange: false)
    .AddJsonFile(
        Path.Combine(projectDirectory, $"appsettings.{environmentName}.json"),
        optional: true,
        reloadOnChange: false)
    .AddEnvironmentVariables();

builder.Services.AddInfrastructure(builder.Configuration);

using var host = builder.Build();
using var scope = host.Services.CreateScope();

var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();
var connectionString = configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException(
        "Connection string 'DefaultConnection' is not configured for IGenServer.DbSync.");
}

var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
await dbContext.Database.MigrateAsync();

var adminSeed = configuration.GetSection(AdminSeedOptions.SectionName).Get<AdminSeedOptions>();

if (adminSeed is { Enabled: true })
{
    if (string.IsNullOrWhiteSpace(adminSeed.Email) || string.IsNullOrWhiteSpace(adminSeed.Password))
    {
        throw new InvalidOperationException("Admin seed is enabled, but email/password are not configured.");
    }

    var adminExists = await dbContext.Users.AnyAsync(
        user => user.Email == adminSeed.Email,
        CancellationToken.None);

    if (!adminExists)
    {
        var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

        dbContext.Users.Add(new User
        {
            FullName = adminSeed.FullName,
            Email = adminSeed.Email,
            PasswordHash = passwordHasher.HashPassword(adminSeed.Password),
            Role = UserRole.Admin
        });

        await dbContext.SaveChangesAsync();
    }
}

internal sealed class AdminSeedOptions
{
    public const string SectionName = "AdminSeed";

    public bool Enabled { get; init; }

    public string FullName { get; init; } = "Super Admin";

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;
}
