using System;
using Common.DelegateHandlers;
using Common.Services;
using Common.Services.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Telegram.Bot;

namespace Common.DIContainerCore
{
    public static class ContainerExtension
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration, string connectionString = null)
        {
            services.AddScoped<ITelegramBotClient>(x => new TelegramBotClient(configuration["Telegram:Token"]));

            services.AddHttpClient<IWeatherService, WeatherService>(options =>
            {
                options.BaseAddress = new Uri(configuration["OpenWeatherMap:ApiEndpoint"]);
                options.Timeout = TimeSpan.FromSeconds(30);
            }).AddHttpMessageHandler(options => new OpenWeatherMapDelegateHandler(configuration));
        }
    }
}