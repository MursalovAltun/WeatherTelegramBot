using System.Threading.Tasks;
using Common.DTO;
using Common.Services;
using Common.Services.Infrastructure.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utf8Json;

namespace Common.WebApiCore.Controllers
{
    /// <summary>
    /// This controller is for managing bot
    /// </summary>
    [Route("Account")]
    public class AccountController : ControllerBase
    {
        private readonly ITelegramService _telegramService;

        public AccountController(ITelegramService telegramService)
        {
            this._telegramService = telegramService;
        }

        /// <summary>
        /// Gets info about bot
        /// </summary>
        /// <returns>UserDTO object</returns>
        [HttpGet("GetBotInfo")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public async Task<IActionResult> GetBotInfo()
        {
            var bot = await this._telegramService.GetMeAsync();
            return Ok(bot);
        }
    }
}