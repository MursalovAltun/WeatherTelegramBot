using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.Services
{
    public interface ICommandService
    {
        Task HandleStart(Message message);

        Task HandleStop(Message message);

        Task HandleCurrentWeatherInfo(Message message);

        Task HandleUnknown();
    }
}