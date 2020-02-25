using System;
using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services;
using Common.Utils;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Common.Services
{
    public class WebhookHandlerService : IWebhookHandlerService
    {
        private readonly ICommandService _commandService;

        public WebhookHandlerService(ICommandService commandService)
        {
            this._commandService = commandService;
        }

        public async Task HandleTextMessage(Message message)
        {
            switch (message.Text)
            {
                case TelegramCommand.Start:
                    await this._commandService.HandleStart(message);
                    break;
                case TelegramCommand.CurrentWeather:
                    await this._commandService.HandleCurrentWeatherInfo(message);
                    break;
                case TelegramCommand.Stop:
                    await this._commandService.HandleStop(message);
                    break;
                default:
                    await this._commandService.HandleUnknown();
                    break;
            }
        }

        public async Task HandleLocationMessage(Message message)
        {
            await this._commandService.HandleCurrentWeatherInfo(message);
        }
    }
}