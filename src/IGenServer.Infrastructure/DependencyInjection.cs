// src/IGenServer.Infrastructure/DependencyInjection.cs
using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Features.Auth.Commands.RegisterUser;
using IGenServer.Infrastructure.Authentication;
using IGenServer.Persistance.Data;
using IGenServer.Persistance.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace IGenServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository,UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<RegisterUserCommandHandler>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        return services;
    }

}
