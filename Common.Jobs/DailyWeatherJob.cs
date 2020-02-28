using System;
using System.Threading.Tasks;
using Common.Services.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Telegram.Bot;

namespace Common.Jobs
{
    [DisallowConcurrentExecution]
    public class DailyWeatherJob : IJob
    {
        private readonly IServiceProvider _provider;

        public DailyWeatherJob(IServiceProvider provider)
        {
            this._provider = provider;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            using (var scope = this._provider.CreateScope())
            {
                var subscriberService = scope.ServiceProvider.GetService<ISubscriberService>();
                var telegramBotClient = scope.ServiceProvider.GetService<ITelegramBotClient>();
                var weatherService = scope.ServiceProvider.GetService<IWeatherService>();
                var subscribers = await subscriberService.GetDailyReceivers();
                foreach (var subscriber in subscribers)
                {
                    var utcNow = DateTime.UtcNow;
                    var offset = TimeSpan.FromSeconds(subscriber.UtcOffset);
                    var subscriberLocalTime = utcNow + offset;
                    // Check if now 9 o'clock for user
                    if (subscriberLocalTime.Hour == 9)
                    {
                        var weatherDto = await weatherService.GetCurrentWeatherByCity(subscriber.City);
                        var responseCaption = weatherService.GetReadableInfo(weatherDto);
                        var responsePhoto = weatherService.GetIconUrl(weatherDto);
                        await telegramBotClient.SendPhotoAsync(subscriber.ChatId, responsePhoto, responseCaption);
                    }
                }
            }
        }
    }
}