using IGenServer.Application;
using IGenServer.Application.Abstractions.Authentication;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using IGenServer.Application.Abstractions.Persistence;
using IGenServer.Infrastructure.Authentication;
using IGenServer.Persistance.Data;
using IGenServer.Persistance.Repositories;
using IGenServer.Persistence.Repositories;
using IGenServer.Persistence.Repositories.Dapper;
using Microsoft.EntityFrameworkCore;

namespace IGenServer.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddScoped<IRefreshTokenService, RefreshTokenService>();
        services.AddScoped<IStudentReadRepository, StudentReadRepository>();
        services.AddScoped<IStudentHabitRepository, StudentHabitRepository>();
        services.AddScoped<IProgramRepository, ProgramRepository>();
        services.AddScoped<ISubjectRepository, SubjectRepository>();
        services.AddScoped<ISubjectCurriculumRepository, SubjectCurriculumRepository>();
        services.AddScoped<IAdminCourseRepository, AdminCourseRepository>();
        services.AddScoped<IStudentCourseRepository, StudentCourseRepository>();
        services.AddScoped<IAdminCurriculumReadRepository, AdminCurriculumReadRepository>();

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(AssemblyReference).Assembly));

        return services;
    }
}
