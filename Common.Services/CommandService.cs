using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using Common.Extensions;
using Common.Services.Infrastructure.Services;
using Common.Utils;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common.Services
{
    public class CommandService : ICommandService
    {
        private readonly ISubscriberService _subscriberService;
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IMapper _mapper;
        private readonly IWeatherService _weatherService;

        public CommandService(ISubscriberService subscriberService,
                              ITelegramBotClient telegramBotClient,
                              IMapper mapper,
                              IWeatherService weatherService)
        {
            this._subscriberService = subscriberService;
            this._telegramBotClient = telegramBotClient;
            this._mapper = mapper;
            this._weatherService = weatherService;
        }

        public async Task HandleStart(Message message)
        {
            var user = message.From;
            var chatId = message.Chat.Id;
            var subscriber = await this._subscriberService.GetByUsername(user.Username);
            var rkm = new ReplyKeyboardMarkup
            {
                Keyboard = this.CreateKeyboard(),
                ResizeKeyboard = true
            };
            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
            await Task.Delay(1000 * 1);
            var responseMessage = $"Hi {subscriber.FirstName}\\. Nice to see you here\\! " +
                                 "You can just send your current location to me to know a weather for now\\!";
            await this._telegramBotClient.SendTextMessageAsync(chatId, responseMessage, ParseMode.MarkdownV2, replyMarkup: rkm);
        }

        public async Task HandleStop(Message message)
        {
            var user = message.From;
            var chatId = message.Chat.Id;
            var subscriber = await this._subscriberService.GetByUsername(user.Username);
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
            var subscriber = await this._subscriberService.GetByUsername(message.GetUser().Username);
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
            var subscriber = await this._subscriberService.GetByUsername(message.GetUser().Username);
            subscriber.WaitingFor = null;
            await this._subscriberService.Edit(subscriber);
        }

        public async Task HandleTerminate(Message message)
        {
            var chatId = message.GetChatId();

            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);

            const string response = "This operation is terminated!";

            var subscriber = await this._subscriberService.GetByUsername(message.GetUser().Username);
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

        private IEnumerable<KeyboardButton[]> CreateKeyboard()
        {
            return new[]
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
                }
                //new[]
                //{
                //    new KeyboardButton
                //    {
                //        Text = "🚫 Stop"
                //    }
                //}
            };
        }
    }
}