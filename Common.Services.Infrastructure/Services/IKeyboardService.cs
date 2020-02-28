using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.Services
{
    public interface IKeyboardService
    {
        Task CreateSettings(Message message);

        Task CreateMain(Message message);

        Task CreateGetGeneralInfo(Message message);
    }
}