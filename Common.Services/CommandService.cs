using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Common.DTO;
using Common.Extensions;
using Common.Services.Infrastructure.Services;
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
            var isFirstStart = false;
            var isRecovered = false;
            var subscriber = await this._subscriberService.GetByUsername(user.Username);
            if (subscriber is null)
            {
                isFirstStart = true;
                subscriber = await this._subscriberService.Edit(this._mapper.Map<SubscriberDTO>(user));
            }
            else if (subscriber.IsDelete)
            {
                isRecovered = true;
                await this._subscriberService.Recover(subscriber.Id);
            }
            var rkm = new ReplyKeyboardMarkup
            {
                Keyboard = this.CreateKeyboard(),
                ResizeKeyboard = true
            };
            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
            await Task.Delay(1000 * 1);
            string responseMessage;
            if (isRecovered)
            {
                responseMessage = $"Hi {subscriber.FirstName}\\. Nice to see you here\\! " +
                                  "You can just send your current location to me to know a weather for now\\!";
            }
            else
            {
                responseMessage = $"Hi {subscriber.FirstName}\\. Nice to see you here\\! " +
                                  "You can just send your current location to me to know a weather for now\\!";
            }
            await this._telegramBotClient.SendTextMessageAsync(
                new ChatId(chatId),
                responseMessage,
                ParseMode.MarkdownV2,
                replyMarkup: rkm);
        }

        public async Task HandleStop(Message message)
        {
            var user = message.From;
            var chatId = message.Chat.Id;
            var subscriber = await this._subscriberService.GetByUsername(user.Username);
            await this._subscriberService.Delete(subscriber.Id);
            await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
            await Task.Delay(1000 * 1);
            await this._telegramBotClient.SendTextMessageAsync(
                new ChatId(chatId),
                $"Good bye {subscriber.FirstName} 🖐. We would like to see you again! ✌");
        }

        public async Task HandleCurrentWeatherInfo(Message message)
        {
            var chatId = message.GetChatId();
            await this._telegramBotClient.SendChatActionAsync(new ChatId(chatId), ChatAction.FindLocation);
            await Task.Delay(1000 * 1);
            //this._telegramBotClient.MakeRequestAsync()
            var response = await this._weatherService.GetCurrentWeatherByLocation(message.Location);
            await this._telegramBotClient.SendTextMessageAsync(new ChatId(chatId), response);
        }

        public Task HandleUnknown()
        {
            throw new NotImplementedException();
        }

        private IEnumerable<KeyboardButton[]> CreateKeyboard()
        {
            return new[]
            {
                new[]
                {
                    new KeyboardButton
                    {
                        Text = "☁ Current weather by location",
                        RequestLocation = true
                    }
                },
                new[]
                {
                    new KeyboardButton
                    {
                        Text = "🚫 Stop"
                    }
                }
            };
        }
    }
}