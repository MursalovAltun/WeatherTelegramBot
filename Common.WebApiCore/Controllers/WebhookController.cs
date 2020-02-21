using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    /// <summary>
    /// This controller is using to manage webhooks
    /// </summary>
    [Route("Webhook")]
    public class WebhookController : ControllerBase
    {
        /// <summary>
        /// Endpoint to handle telegram webhooks
        /// </summary>
        /// <returns></returns>
        [Route("Handle")]
        public IActionResult HandleWebhook()
        {
            return Ok();
        }
    }
}