using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.Services
{
    public interface IWebhookHandlerService
    {
        Task HandleTextMessage(Message message);

        Task HandleLocationMessage(Message message);

        Task HandleCallBackQuery(CallbackQuery callbackQuery);
    }
}