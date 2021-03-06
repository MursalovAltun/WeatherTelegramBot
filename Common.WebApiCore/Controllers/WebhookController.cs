﻿using System;
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
        private readonly IWebhookHandlerService _webhookHandlerService;

        public WebhookController(ITelegramBotClient telegramBotClient,
                                 IWebhookHandlerService webhookHandlerService)
        {
            this._telegramBotClient = telegramBotClient;
            this._webhookHandlerService = webhookHandlerService;
        }

        /// <summary>
        /// Endpoint to handle telegram webhooks
        /// </summary>
        /// <returns></returns>
        [HttpPost("Handle")]
        [ApiExplorerSettings(IgnoreApi = true)]
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
                else if (update.Type == UpdateType.CallbackQuery)
                {
                    await this._webhookHandlerService.HandleCallBackQuery(update.CallbackQuery);
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
#if !DEBUG
            var baseUrl = "https://telegramm-weather-bot.herokuapp.com";
#else
            // ngrok in localhost
            var baseUrl = "https://c1ffeac7.ngrok.io";
#endif
            await this._telegramBotClient.SetWebhookAsync($"{baseUrl}/api/Webhook/Handle", allowedUpdates: new List<UpdateType>()
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