using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Common.Extensions;
using Common.Services.Infrastructure.Services;
using Common.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;

namespace Common.WebApiCore.Controllers
{
    /// <summary>
    /// This controller is using to manage webhooks
    /// </summary>
    [Route("Webhook")]
    public class WebhookController : ControllerBase
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IWeatherService _weatherService;

        public WebhookController(ITelegramBotClient telegramBotClient,
                                 IWeatherService weatherService)
        {
            this._telegramBotClient = telegramBotClient;
            this._weatherService = weatherService;
        }
        /// <summary>
        /// Endpoint to handle telegram webhooks
        /// </summary>
        /// <returns></returns>
        [HttpPost("Handle")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> HandleWebhook(Update update)
        {
            try
            {
                var request = this.HttpContext.Request;
                var stream = new StreamReader(request.Body);
                var body = await stream.ReadToEndAsync();
                update = JsonConvert.DeserializeObject<Update>(body);
                if (update.Type == UpdateType.Message)
                {
                    if (update.Message.Type == MessageType.Location)
                    {
                        var response =
                            await this._weatherService.GetCurrentWeatherByLocation(update.Message.Location.Longitude,
                                update.Message.Location.Latitude);
                        var result =
                            await this._telegramBotClient.SendTextMessageAsync(
                                new ChatId(update.Message.Chat.Id), response);
                    }
                    else if (update.Message.Type == MessageType.Text)
                    {
                        switch (update.Message.Text)
                        {
                            case TelegramCommand.Start:
                                {
                                    var chatId = update.Message.Chat.Id;
                                    var keyboardButtons = new KeyboardButton[][]
                                    {
                                        new KeyboardButton[]
                                        {
                                            new KeyboardButton
                                            {
                                                Text = "\U00002601 Current weather by location",
                                                RequestLocation = true
                                            }
                                        },
                                        new KeyboardButton[]
                                        {
                                            new KeyboardButton
                                            {
                                                Text = "\U0001F6AB Stop"
                                            }
                                        }
                                    };
                                    var rkm = new ReplyKeyboardMarkup
                                    {
                                        Keyboard = keyboardButtons,
                                        ResizeKeyboard = true
                                    };
                                    await this._telegramBotClient.SendChatActionAsync(chatId, ChatAction.Typing);
                                    await Task.Delay(1000 * 1);
                                    var result = await this._telegramBotClient.SendTextMessageAsync(
                                        new ChatId(chatId),
                                        $"Hi {update.Message.From.FirstName}\\. Nice to see you here\\! " +
                                            "You can just send your current location to me to know a weather for now\\!",
                                        ParseMode.MarkdownV2, replyMarkup: rkm);
                                    break;
                                }
                            case TelegramCommand.CurrentWeather:
                                if (update.Message.Type == MessageType.Location)
                                {
                                    
                                }

                                break;
                        }
                    }
                }

                return Ok();
            }
            catch (Exception e)
            {
                return Ok();
            }
        }

        /// <summary>
        /// Gets info about webhook state
        /// </summary>
        /// <returns></returns>
        [HttpGet("Info")]
        public async Task<IActionResult> GetWebHookInfo()
        {
            var info = await this._telegramBotClient.GetWebhookInfoAsync();
            return Ok(info);
        }

        [HttpPost("Reset")]
        public async Task<IActionResult> ResetWebhook()
        {
            await this._telegramBotClient.DeleteWebhookAsync();
            var baseUrl = this.HttpContext.GetBaseUrl();
            await this._telegramBotClient.SetWebhookAsync($"{"https://f487056c.ngrok.io"}/api/Webhook/Handle", allowedUpdates: new List<UpdateType>()
            {
                UpdateType.CallbackQuery,
                UpdateType.ChannelPost,
                UpdateType.Message,
                UpdateType.Unknown,
                UpdateType.EditedMessage,
                UpdateType.EditedChannelPost
            });
            return Ok();
        }
    }
}