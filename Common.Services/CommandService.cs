using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using Common.Extensions;
using Common.Services.Infrastructure.Services;
using Common.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common.Services
{
    public class CommandService : ICommandService
    {
        private readonly ISubscriberService _subscriberService;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IMapper _mapper;
        private readonly IWeatherService _weatherService;
        private readonly ISubscriberSettingsService _subscriberSettingsService;
        private readonly ITimezoneService _timezoneService;
        private readonly IGeoCodeService _geoCodeService;

        public CommandService(ISubscriberService subscriberService,
                              ITelegramBotClient telegramBotClient,
                              IMapper mapper,
                              IWeatherService weatherService,
                              ISubscriberSettingsService subscriberSettingsService,
                              ITimezoneService timezoneService,
                              IGeoCodeService geoCodeService)
        {
            this._subscriberService = subscriberService;
            this._telegramBotClient = telegramBotClient;
            this._mapper = mapper;
            this._weatherService = weatherService;
            this._subscriberSettingsService = subscriberSettingsService;
            this._timezoneService = timezoneService;
            this._geoCodeService = geoCodeService;
        }

        public async Task HandleStart(Message message)
        {
            var chatId = message.Chat.Id;

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());

            var responseMessage = $"Hi {subscriber.FirstName}. Nice to see you here!";
            await this._telegramBotClient.SendTextMessageAsync(chatId, responseMessage);
        }

        public async Task HandleStop(Message message)
        {
            var chatId = message.Chat.Id;
            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());
            await this._subscriberService.Delete(subscriber.Id);
            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
            await Task.Delay(1000 * 1);
            await this._telegramBotClient.SendTextMessageAsync(chatId, $"Good bye {subscriber.FirstName} 🖐. We would like to see you again! ✌");
        }

        public async Task HandleCurrentWeatherInfoByLocation(Message message)
        {
            var chatId = message.GetChatId();
            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.FindLocation);
            await Task.Delay(1000 * 1);
            var response = await this._weatherService.GetCurrentWeatherByLocation(message.Location);
            var responseMessage = this._weatherService.GetReadableInfo(response);
            var responseCaption = this._weatherService.GetIconUrl(response);
            await this._telegramBotClient.SendPhotoAsync(chatId, responseCaption, responseMessage);
        }

        public async Task HandleCurrentWeatherInfoByZipCode(Message message)
        {
            var chatId = message.GetChatId();
            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
            await this._telegramBotClient.SendTextMessageAsync(chatId, "Please provide your zip code, for example: 00100,fi\n\n" +
                                                                           "To terminate this operation please, just type /terminate",
                                                               ParseMode.Markdown, replyToMessageId: message.MessageId);
        }

        public async Task HandleCurrentWeatherInfoByZipCodeAnswer(Message message)
        {
            var chatId = message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.FindLocation);

            var response = await this._weatherService.GetCurrentWeatherByZipCode(message.Text);
            var responseMessage = this._weatherService.GetReadableInfo(response);
            var responseCaption = this._weatherService.GetIconUrl(response);
            await this._telegramBotClient.SendPhotoAsync(chatId, responseCaption, responseMessage);
            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());
            subscriber.WaitingFor = null;
            await this._subscriberService.Edit(subscriber);
        }

        public async Task HandleCurrentWeatherInfoByCity(Message message)
        {
            var chatId = message.GetChatId();
            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
            await this._telegramBotClient.SendTextMessageAsync(chatId, "Please provide city name, for example: London\n\n" +
                                                                           "You can additionally set state or country through \",\" for example:\n" +
                                                                           "Charlotte,NC,US or London,UK\n\n" +
                                                                           "To terminate this operation please, just type /terminate",
                ParseMode.Markdown, replyToMessageId: message.MessageId);
        }

        public async Task HandleCurrentWeatherInfoByCityAnswer(Message message)
        {
            var chatId = message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.FindLocation);

            var response = await this._weatherService.GetCurrentWeatherByCity(message.Text);
            var responseMessage = this._weatherService.GetReadableInfo(response);
            var responseCaption = this._weatherService.GetIconUrl(response);
            await this._telegramBotClient.SendPhotoAsync(chatId, responseCaption, responseMessage);
            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());
            subscriber.WaitingFor = null;
            await this._subscriberService.Edit(subscriber);
        }

        public async Task HandleDailyForecastsSettings(Message message)
        {
            var chatId = message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.FindLocation);

            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());
            var subscriberSettings = await this._subscriberSettingsService.GetBySubscriberId(subscriber.Id);

            var isDailyForecastsEnabled = subscriberSettings != null && subscriberSettings.IsReceiveDailyWeather;

            var inlineButtons = new[]
            {
                new []
                {
                    new InlineKeyboardButton
                    {
                        Text = "✔️ Enable",
                        CallbackData = TelegramCommand.EnableDailyForecasts
                    },
                    new InlineKeyboardButton
                    {
                        Text = "❌ Disable",
                        CallbackData = TelegramCommand.DisableDailyForecasts
                    }
                }
            };

            var ikm = new InlineKeyboardMarkup(inlineButtons);

            await this._telegramBotClient.SendTextMessageAsync(chatId, $"Current daily forecasts state: {(isDailyForecastsEnabled ? "✔️" : "❌")}", replyMarkup: ikm);
        }

        public async Task HandleDailyForecastsSettingsAnswer(CallbackQuery callbackQuery)
        {
            var chatId = callbackQuery.Message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.FindLocation);

            var isEnable = false || callbackQuery.Data == TelegramCommand.EnableDailyForecasts;

            var subscriber = await this._subscriberService.GetByTelegramUserId(callbackQuery.Message.GetUserTelegramId());
            var subscriberSettings = await this._subscriberSettingsService.GetBySubscriberId(subscriber.Id);
            if (subscriberSettings is null)
            {
                subscriberSettings = new SubscriberSettingsDTO
                {
                    SubscriberId = subscriber.Id
                };
            }
            subscriberSettings.IsReceiveDailyWeather = isEnable;
            await this._subscriberSettingsService.Edit(subscriberSettings);

            await this._telegramBotClient.SendTextMessageAsync(chatId, $"✔️ Daily weather forecasts are successfully {(isEnable ? "enabled" : "disabled")}!", replyMarkup: this.CreateKeyboard());
        }

        public async Task HandleMeasureSystemSettings(Message message)
        {
            var chatId = message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.FindLocation);

            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());
            var subscriberSettings = await this._subscriberSettingsService.GetBySubscriberId(subscriber.Id);

            var currentMeasureSystem = subscriberSettings.MeasureSystem;

            var inlineButtons = new[]
            {
                new []
                {
                    new InlineKeyboardButton
                    {
                        Text = "Imperial",
                        CallbackData = TelegramCommand.MeasureImperial
                    },
                    new InlineKeyboardButton
                    {
                        Text = "Metric",
                        CallbackData = TelegramCommand.MeasureMetric
                    }
                }
            };

            var ikm = new InlineKeyboardMarkup(inlineButtons);

            await this._telegramBotClient.SendTextMessageAsync(chatId, $"Current measure system: {currentMeasureSystem}", replyMarkup: ikm);
        }

        public async Task HandleMeasureSystemSettingsAnswer(CallbackQuery callbackQuery)
        {
            var chatId = callbackQuery.Message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.FindLocation);

            var measureSystem = string.Empty;
            if (callbackQuery.Data == TelegramCommand.MeasureMetric)
            {
                measureSystem = "metric";
            }
            else if(callbackQuery.Data == TelegramCommand.MeasureImperial)
            {
                measureSystem = "imperial";
            }

            var subscriber = await this._subscriberService.GetByTelegramUserId(callbackQuery.Message.GetUserTelegramId());
            var subscriberSettings = await this._subscriberSettingsService.GetBySubscriberId(subscriber.Id);
            if (subscriberSettings is null)
            {
                subscriberSettings = new SubscriberSettingsDTO
                {
                    SubscriberId = subscriber.Id
                };
            }
            subscriberSettings.MeasureSystem = measureSystem;
            await this._subscriberSettingsService.Edit(subscriberSettings);

            await this._telegramBotClient.SendTextMessageAsync(chatId, $"✔️ Measure system is successfully changed to {measureSystem}!", replyMarkup: this.CreateKeyboard());
        }

        public async Task HandleGetLocation(Message message)
        {
            var chatId = message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.FindLocation);

            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());

            var timezone = await this._timezoneService.GetTimezoneByLocation(message.Location);
            var geoCode = await this._geoCodeService.GetCityByLocation(message.Location);
            subscriber.City = geoCode.City;
            subscriber.UtcOffset = timezone.GmtOffset;
            subscriber.WaitingFor = null;
            await this._subscriberService.Edit(subscriber);

            await this._telegramBotClient.SendTextMessageAsync(chatId,
                "All set, now you can use all available commands!", replyMarkup: this.CreateKeyboard());
        }

        public async Task HandleTerminate(Message message)
        {
            var chatId = message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);

            const string response = "This operation is terminated!";

            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());
            subscriber.WaitingFor = null;
            await this._subscriberService.Edit(subscriber);

            await this._telegramBotClient.SendTextMessageAsync(chatId, response);
        }

        public async Task HandleUnknown(Message message)
        {
            var chatId = message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
            await this._telegramBotClient.SendTextMessageAsync(chatId, "Sorry, but I cannot understand you :(");
        }

        private ReplyKeyboardMarkup CreateKeyboard()
        {
            var keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton
                    {
                        Text = "📍 Current weather by location",
                        RequestLocation = true
                    }
                },
                new []
                {
                    new KeyboardButton
                    {
                        Text = TelegramCommand.CurrentWeatherByZipCodeButton
                    }
                },
                new []
                {
                    new KeyboardButton
                    {
                        Text = TelegramCommand.CurrentWeatherByCityButton
                    }
                },
                new[]
                {
                    new KeyboardButton {
                        Text = "⚙️ Settings"
                    }
                }
                //new[]
                //{
                //    new KeyboardButton
                //    {
                //        Text = "🚫 Stop"
                //    }
                //}
            };
            return new ReplyKeyboardMarkup
            {
                Keyboard = keyboard,
                ResizeKeyboard = true
            };
        }
    }
}