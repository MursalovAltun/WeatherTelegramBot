using System.Threading.Tasks;
using Common.Extensions;
using Common.Services.Infrastructure.Services;
using Common.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common.Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IKeyboardService _keyboardService;
        private readonly ISubscriberService _subscriberService;
        private readonly ISubscriberSettingsService _subscriberSettingsService;

        public SettingsService(ITelegramBotClient telegramBotClient,
                               IKeyboardService keyboardService,
                               ISubscriberService subscriberService,
                               ISubscriberSettingsService subscriberSettingsService)
        {
            this._telegramBotClient = telegramBotClient;
            this._keyboardService = keyboardService;
            this._subscriberService = subscriberService;
            this._subscriberSettingsService = subscriberSettingsService;
        }

        public async Task HandleMeasureSystemSettings(Message message)
        {
            var chatId = message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);

            var subscriber = await this._subscriberService.GetByTelegramUserId(message.GetUserTelegramId());
            var subscriberSettings = await this._subscriberSettingsService.GetBySubscriberId(subscriber.Id);

            var isDailyForecastsEnabled = subscriberSettings != null && subscriberSettings.IsReceiveDailyWeather;

            var inlineButtons = new[]
            {
                new []
                {
                    new InlineKeyboardButton
                    {
                        Text = "Imperial",
                        CallbackData = TelegramCommand.EnableDailyForecasts
                    },
                    new InlineKeyboardButton
                    {
                        Text = "metric",
                        CallbackData = TelegramCommand.DisableDailyForecasts
                    }
                }
            };

            var ikm = new InlineKeyboardMarkup(inlineButtons);

            await this._telegramBotClient.SendTextMessageAsync(chatId, $"Current daily forecasts state: {(isDailyForecastsEnabled ? "✔️" : "❌")}", replyMarkup: ikm);
        }
    }
}