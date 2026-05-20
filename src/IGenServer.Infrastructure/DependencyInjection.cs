// src/IGenServer.Infrastructure/DependencyInjection.cs

using FluentValidation;
using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Application.Common.Behaviors;
using IGenServer.Infrastructure.Authentication;
using IGenServer.Persistance.Data;
using IGenServer.Persistance.Repositories;
using IGenServer.Persistence.Repositories;
using MediatR;
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
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IStudentReadRepository, StudentReadRepository>();
        services.AddScoped<IStudentHabitRepository, StudentHabitRepository>();
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(
                typeof(IGenServer.Application.AssemblyReference).Assembly));

        services.AddValidatorsFromAssembly(
            typeof(IGenServer.Application.AssemblyReference).Assembly);

        services.AddTransient(
            typeof(IPipelineBehavior<,>),
            typeof(ValidationBehavior<,>));

        return services;
    }
}