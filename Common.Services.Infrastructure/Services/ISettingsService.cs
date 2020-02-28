using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.Services
{
    public interface ISettingsService
    {
        Task HandleMeasureSystemSettings(Message message);
    }
}