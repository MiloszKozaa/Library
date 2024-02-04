using FluentValidation.AspNetCore;
using Library.Application;
using Library.Application.Services.Persistence.Repositories;
using Library.Infrastructure.Persistence;
using Library.Infrastructure.Persistence.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Library.Infrastructure;
public static class DependencyConfiguration
{
    public static IServiceCollection InfrastructureConfiguration(this IServiceCollection services, IConfiguration configuration)
    {
        //services.AddFluentValidation(config => config.RegisterValidatorsFromAssembly(typeof(ApplicationEntryPoint).Assembly));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddMediatR(config => config.RegisterServicesFromAssembly(typeof(ApplicationEntryPoint).Assembly));
        return services.AddDbContext<LibraryDbContext>(config => config.UseSqlServer(configuration.GetConnectionString("library")));
    }
}
