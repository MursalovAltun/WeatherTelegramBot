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
        private readonly IKeyboardService _keyboardService;

        public WebhookHandlerService(ICommandService commandService,
                                     ISubscriberService subscriberService,
                                     IMapper mapper,
                                     IKeyboardService keyboardService)
        {
            this._commandService = commandService;
            this._subscriberService = subscriberService;
            this._mapper = mapper;
            this._keyboardService = keyboardService;
        }

        public async Task HandleTextMessage(Message message)
        {
            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());

            if (subscriber is null)
            {
                var user = this._mapper.Map<SubscriberDTO>(message.GetUser());
                user.ChatId = message.GetChatId();
                subscriber = await this._subscriberService.Edit(user);
            }
            else if (subscriber.ChatId != message.GetChatId())
            {
                subscriber.ChatId = message.GetChatId();
                subscriber = await this._subscriberService.Edit(subscriber);
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
                        await this._keyboardService.CreateGetGeneralInfo(message);
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
                    case TelegramCommand.SettingsMenu:
                        await this._keyboardService.CreateSettings(message);
                        break;
                    case TelegramCommand.GoBackSettings:
                        await this._keyboardService.CreateMain(message);
                        break;
                    case TelegramCommand.DailyForecasts:
                        await this._commandService.HandleDailyForecastsSettings(message);
                        break;
                    case TelegramCommand.MeasureSystem:
                        await this._commandService.HandleMeasureSystemSettings(message);
                        break;
                    case TelegramCommand.Stop:
                    case TelegramCommand.StopButton:
                        await this._commandService.HandleStop(message);
                        break;
                    case TelegramCommand.Terminate:
                        break;
                    default:
                        await this._commandService.HandleUnknown(message);
                        await this._keyboardService.CreateMain(message);
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
            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());
            if (subscriber.WaitingFor != null)
            {
                switch (subscriber.WaitingFor)
                {
                    case TelegramCommand.GetLocationInfo:
                        await this._commandService.HandleGetLocation(message);
                        break;
                }
            }
            else
            {
                await this._commandService.HandleCurrentWeatherInfoByLocation(message);
            }
        }

        public async Task HandleCallBackQuery(CallbackQuery callbackQuery)
        {
            switch (callbackQuery.Data)
            {
                case TelegramCommand.EnableDailyForecasts:
                case TelegramCommand.DisableDailyForecasts:
                    await this._commandService.HandleDailyForecastsSettingsAnswer(callbackQuery);
                    break;
                case TelegramCommand.MeasureImperial:
                case TelegramCommand.MeasureMetric:
                    await this._commandService.HandleMeasureSystemSettingsAnswer(callbackQuery);
                    break;
            }
        }
    }
}