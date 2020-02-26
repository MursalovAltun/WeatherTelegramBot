using System;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using Common.Extensions;
using Common.Services.Infrastructure.Services;
using Common.Utils;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Common.Services
{
    public class WebhookHandlerService : IWebhookHandlerService
    {
        private readonly ICommandService _commandService;
        private readonly ISubscriberService _subscriberService;
        private readonly IMapper _mapper;

        public WebhookHandlerService(ICommandService commandService,
                                     ISubscriberService subscriberService,
                                     IMapper mapper)
        {
            this._commandService = commandService;
            this._subscriberService = subscriberService;
            this._mapper = mapper;
        }

        public async Task HandleTextMessage(Message message)
        {
            var subscriber = await this._subscriberService.GetByUsername(message.GetUser().Username);

            if (subscriber is null)
            {
                subscriber = await this._subscriberService.Edit(this._mapper.Map<SubscriberDTO>(message.GetUser()));
            }
            else if (subscriber.IsDelete)
            {
                await this._subscriberService.Recover(subscriber.Id);
            }
            if (subscriber.WaitingFor == null)
            {
                switch (message.Text)
                {
                    case TelegramCommand.Start:
                        await this._commandService.HandleStart(message);
                        break;
                    case TelegramCommand.CurrentWeatherByZipCode:
                    case TelegramCommand.CurrentWeatherByZipCodeButton:
                        subscriber.WaitingFor = TelegramCommand.CurrentWeatherByZipCode;
                        await this._subscriberService.Edit(subscriber);
                        await this._commandService.HandleCurrentWeatherInfoByZipCode(message);
                        break;
                    case TelegramCommand.CurrentWeatherByCity:
                    case TelegramCommand.CurrentWeatherByCityButton:
                        subscriber.WaitingFor = TelegramCommand.CurrentWeatherByCity;
                        await this._subscriberService.Edit(subscriber);
                        await this._commandService.HandleCurrentWeatherInfoByCity(message);
                        break;
                    case TelegramCommand.Stop:
                    case TelegramCommand.StopButton:
                        await this._commandService.HandleStop(message);
                        break;
                    case TelegramCommand.Terminate:
                        break;
                    default:
                        await this._commandService.HandleUnknown(message);
                        break;
                }
            }
            else
            {
                if (message.Text != TelegramCommand.Terminate)
                {
                    switch (subscriber.WaitingFor)
                    {
                        case TelegramCommand.CurrentWeatherByZipCode:
                            await this._commandService.HandleCurrentWeatherInfoByZipCodeAnswer(message);
                            break;
                        case TelegramCommand.CurrentWeatherByCity:
                            await this._commandService.HandleCurrentWeatherInfoByCityAnswer(message);
                            break;
                    }
                }
                else
                {
                    // Terminate operation
                    await this._commandService.HandleTerminate(message);
                }
            }
        }

        public async Task HandleLocationMessage(Message message)
        {
            await this._commandService.HandleCurrentWeatherInfoByLocation(message);
        }
    }
}