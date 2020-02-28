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
    public class KeyboardService : IKeyboardService
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly ISubscriberService _subscriberService;

        public KeyboardService(ITelegramBotClient telegramBotClient,
                               ISubscriberService subscriberService)
        {
            this._telegramBotClient = telegramBotClient;
            this._subscriberService = subscriberService;
        }

        public async Task CreateSettings(Message message)
        {
            var chatId = message.Chat.Id;
            var keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton
                    {
                        Text = TelegramCommand.DailyForecasts
                    }
                },
                new []
                {
                    new KeyboardButton
                    {
                        Text = TelegramCommand.MeasureSystem
                    } 
                }
            };
            var rkm = new ReplyKeyboardMarkup
            {
                Keyboard = keyboard,
                ResizeKeyboard = true
            };

            await this._telegramBotClient.SendTextMessageAsync(chatId, "Available settings provided below: ", replyMarkup: rkm);
        }

        public async Task CreateMain(Message message)
        {
            var chatId = message.Chat.Id;

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
                        Text = TelegramCommand.SettingsMenu
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
            var rkm = new ReplyKeyboardMarkup
            {
                Keyboard = keyboard,
                ResizeKeyboard = true
            };

            await this._telegramBotClient.SendTextMessageAsync(chatId, "Let's try again!", replyMarkup: rkm);
        }

        public async Task CreateGetGeneralInfo(Message message)
        {
            var chatId = message.Chat.Id;

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
            var subscriber = await this._subscriberService.GetByUsername(message.GetUser().Username);
            subscriber.WaitingFor = TelegramCommand.GetLocationInfo;
            await this._subscriberService.Edit(subscriber);

            var keyboard = new[]
            {
                new[]
                {
                    new KeyboardButton
                    {
                        Text = TelegramCommand.GetLocationInfo,
                        RequestLocation = true
                    }
                }
            };
            var rkm = new ReplyKeyboardMarkup
            {
                Keyboard = keyboard,
                ResizeKeyboard = true
            };

            await this._telegramBotClient.SendTextMessageAsync(chatId, "Before we'll start, I need you to send me your location to determine your timezone and city", replyMarkup: rkm);
        }
    }
}