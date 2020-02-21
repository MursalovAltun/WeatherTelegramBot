using System;
using Common.Services;
using Common.Services.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.DIContainerCore
{
    public static class ContainerExtension
    {
        public static void Initialize(IServiceCollection services, IConfiguration configuration, string connectionString = null)
        {
            services.AddHttpClient<ITelegramService, TelegramService>(options =>
            {
                options.BaseAddress = new Uri($"{configuration["Telegram:ApiEndpoint"]}" +
                                              $"{configuration["Telegram:Token"]}" +
                                              $"/");
                options.Timeout = TimeSpan.FromSeconds(50);
            });
        }
    }
}