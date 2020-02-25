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
        private readonly IWebhookHandlerService _webhookHandlerService;

        public WebhookController(ITelegramBotClient telegramBotClient,
                                 IWeatherService weatherService,
                                 IWebhookHandlerService webhookHandlerService)
        {
            this._telegramBotClient = telegramBotClient;
            this._weatherService = weatherService;
            this._webhookHandlerService = webhookHandlerService;
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
                        await this._webhookHandlerService.HandleLocationMessage(update.Message);
                    }
                    else if (update.Message.Type == MessageType.Text)
                    {
                        await this._webhookHandlerService.HandleTextMessage(update.Message);
                    }
                }
                else if (update.Type == UpdateType.InlineQuery)
                {
                    //update.InlineQuery.
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