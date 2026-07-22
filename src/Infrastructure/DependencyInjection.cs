using Business.Abstractions.Persistence;
using Business.Authentication;
using CrossCutting.Exceptions;
using Domain.Instruments;
using Domain.Organizations;
using Domain.Users;
using Infrastructure.Authentication;
using Infrastructure.Persistence;
using Infrastructure.Persistence.Instruments;
using Infrastructure.Persistence.Organizations;
using Infrastructure.Persistence.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(
                configuration.GetConnectionString("Default")));

        JwtOptions jwtOptions = configuration.GetSection(JwtOptions.SectionName)
            .Get<JwtOptions>()
            ?? throw new ConfigurationException("JWT configuration is not present");

        services.Configure<JwtOptions>(
            configuration.GetSection(JwtOptions.SectionName));

        services.AddSingleton(jwtOptions);
        services.AddScoped<DatabaseSeeder>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IPasswordHasher, PasswordHasher>();
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();
        services.AddScoped<IInstrumentDefinitionRepository, InstrumentDefinitionRepository>();

        return services;
    }
}