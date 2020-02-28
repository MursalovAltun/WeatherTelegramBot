using System;
using Common.DataAccess.EFCore;
using Common.DataAccess.EFCore.Repositories;
using Common.DelegateHandlers;
using Common.Services;
using Common.Services.Infrastructure;
using Common.Services.Infrastructure.Repositories;
using Common.Services.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Common.DIContainerCore
{
    public static class ContainerExtension
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration, string connectionString = null)
        {
            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            InitServices(services, configuration);

            InitRepositories(services, configuration);
        }

        private static void InitServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IDataBaseInitializer, DataBaseInitializer>();

            services.AddScoped<ITelegramBotClient>(x => new TelegramBotClient(configuration["Telegram:Token"]));

            services.AddHttpClient<IWeatherService, WeatherService>(options =>
            {
                options.BaseAddress = new Uri(configuration["OpenWeatherMap:ApiEndpoint"]);
                options.Timeout = TimeSpan.FromSeconds(30);
            }).AddHttpMessageHandler(options => new OpenWeatherMapDelegateHandler(configuration));

            services.AddScoped<ISubscriberService, SubscriberService>();

            services.AddScoped<IWebhookHandlerService, WebhookHandlerService>();

            services.AddScoped<ICommandService, CommandService>();

            services.AddScoped<IKeyboardService, KeyboardService>();

            services.AddScoped<ISubscriberSettingsService, SubscriberSettingsService>();

            services.AddHttpClient<ITimezoneService, TimezoneService>(options =>
            {
                options.BaseAddress = new Uri(configuration["TimeZoneDb:ApiEndpoint"]);
                options.Timeout = TimeSpan.FromSeconds(30);
            }).AddHttpMessageHandler(options => new TimeZoneDbDelegateHandler(configuration));

            services.AddHttpClient<IGeoCodeService, GeoCodeService>(options =>
            {
                options.BaseAddress = new Uri(configuration["GeoCodeXYZ:ApiEndpoint"]);
                options.Timeout = TimeSpan.FromSeconds(30);
            }).AddHttpMessageHandler(options => new GeoCodeXYZDelegateHandler(configuration));
        }

        private static void InitRepositories(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ISubscriberRepository, SubscriberRepository>();

            services.AddScoped<ISubscriberSettingsRepository, SubscriberSettingsRepository>();
        }
    }
}