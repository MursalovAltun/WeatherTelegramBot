using System.Threading.Tasks;
using Common.Services.Infrastructure.Services;
using Quartz;
using Telegram.Bot;

namespace Common.Jobs
{
    [DisallowConcurrentExecution]
    public class DailyWeatherJob : IJob
    {
        private readonly IWeatherService _weatherService;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ISubscriberService _subscriberService;

        public DailyWeatherJob(IWeatherService weatherService,
                               ITelegramBotClient telegramBotClient,
                               ISubscriberService subscriberService)
        {
            this._weatherService = weatherService;
            this._telegramBotClient = telegramBotClient;
            this._subscriberService = subscriberService;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var subscribers = await this._subscriberService.GetAll();
            foreach (var subscriber in subscribers)
            {
                var weatherDto = await this._weatherService.GetCurrentWeatherByCity("London");
                var responseCaption = this._weatherService.GetReadableInfo(weatherDto);
                var responsePhoto = this._weatherService.GetIconUrl(weatherDto);
                await this._telegramBotClient.SendPhotoAsync(subscriber.ChatId, responsePhoto, responseCaption);
            }
        }
    }
}