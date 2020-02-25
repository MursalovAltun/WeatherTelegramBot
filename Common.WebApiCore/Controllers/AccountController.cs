using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Telegram.Bot;

namespace Common.WebApiCore.Controllers
{
    /// <summary>
    /// This controller is for managing bot
    /// </summary>
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public AccountController(ITelegramBotClient telegramBotClient)
        {
            this._telegramBotClient = telegramBotClient;
        }

        /// <summary>
        /// Gets info about bot
        /// </summary>
        /// <returns>UserDTO object</returns>
        [HttpGet("GetBotInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBotInfo()
        {
            var bot = await this._telegramBotClient.GetMeAsync();
            return Ok(bot);
        }
    }
}