using SessionReservation.Application.Common.Interfaces;
using SessionReservation.Domain.Common.Interfaces;
using SessionReservation.Infrastructure.IntegrationEvents.BackgroundServices;
using SessionReservation.Infrastructure.IntegrationEvents.Settings;
using SessionReservation.Infrastructure.Persistence;
using SessionReservation.Infrastructure.Persistence.Repositories;
using SessionReservation.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace SessionReservation.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddConfigurations(configuration)
            .AddServices()
            .AddPersistence()
            .AddBackgroundServices();

        return services;
    }

    public static IServiceCollection AddBackgroundServices(this IServiceCollection services)
    {
        services.AddHostedService<ConsumeIntegrationEventsBackgroundService>();

        return services;
    }

    public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions();

        var messageBrokerSettings = new MessageBrokerSettings();
        configuration.Bind(MessageBrokerSettings.Section, messageBrokerSettings);

        services.AddSingleton(Options.Create(messageBrokerSettings));

        return services;
    }

    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDateTimeProvider, SystemDateTimeProvider>();

        return services;
    }

    public static IServiceCollection AddPersistence(this IServiceCollection services)
    {
        services.AddDbContext<SessionReservationDbContext>(options =>
            options.UseSqlite("Data Source = SessionReservation.db"));

        services.AddScoped<IParticipantsRepository, ParticipantsRepository>();
        services.AddScoped<IRoomsRepository, RoomsRepository>();
        services.AddScoped<ISessionsRepository, SessionsRepository>();
        services.AddScoped<ITrainersRepository, TrainersRepository>();

        return services;
    }
}