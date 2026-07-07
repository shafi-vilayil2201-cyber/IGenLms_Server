using IGenServer.Application.Abstractions.Authentication;
using IGenServer.Domain.Entities;
using IGenServer.Domain.Enums;
using IGenServer.Infrastructure;
using IGenServer.Persistance.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddPolicy("LocalDevCors", policy =>
    {
        policy.WithOrigins(
                "http://localhost:5173",
                "http://127.0.0.1:5173",
                "http://localhost:5174",
                "http://127.0.0.1:5174")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        var jwtKey = builder.Configuration["Jwt:Key"];
        var jwtIssuer = builder.Configuration["Jwt:Issuer"];
        var jwtAudience = builder.Configuration["Jwt:Audience"];

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidIssuer = jwtIssuer,
            ValidateAudience = true,
            ValidAudience = jwtAudience,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey ?? string.Empty)),
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });
builder.Services.AddAuthorization();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter your JWT token as: Bearer {token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("LocalDevCors");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    await SeedDemoAccountsAsync(app.Services);
}

app.Run();

static async Task SeedDemoAccountsAsync(IServiceProvider services)
{
    await using var scope = services.CreateAsyncScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    var passwordHasher = scope.ServiceProvider.GetRequiredService<IPasswordHasher>();

    await dbContext.Database.MigrateAsync();

    var demoAccounts = new[]
    {
        new
        {
            Email = "student@upsc.com",
            FullName = "Demo Student",
            Password = "password123",
            Role = UserRole.Student,
            StudentProfile = new StudentProfile
            {
                TargetYear = 2027,
                StudyStreak = 12,
                Rank = 128
            }
        },
        new
        {
            Email = "mentor@upsc.com",
            FullName = "Demo Mentor",
            Password = "password123",
            Role = UserRole.Mentor,
            MentorProfile = new MentorProfile
            {
                ExpertiseJson = "[\"Polity\",\"Ethics\",\"Essay\"]",
                ApprovalStatus = MentorApprovalStatus.Approved
            }
        },
        new
        {
            Email = "admin@igen.com",
            FullName = "Demo Admin",
            Password = "admin123",
            Role = UserRole.Admin
        }
    };

    foreach (var account in demoAccounts)
    {
        var existingUser = await dbContext.Users
            .Include(x => x.StudentProfile)
            .Include(x => x.MentorProfile)
            .FirstOrDefaultAsync(x => x.Email == account.Email);

        if (existingUser is not null)
        {
            continue;
        }

        var user = new User
        {
            FullName = account.FullName,
            Email = account.Email,
            PasswordHash = passwordHasher.HashPassword(account.Password),
            Role = account.Role
        };

        if (account.Role == UserRole.Student)
        {
            user.StudentProfile = account.StudentProfile;
        }
        else if (account.Role == UserRole.Mentor)
        {
            user.MentorProfile = account.MentorProfile;
        }

        dbContext.Users.Add(user);
    }

    await dbContext.SaveChangesAsync();
}
