using System.Threading.Tasks;
using Common.DTO;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.Services
{
    public interface ITimezoneService
    {
        Task<TimezoneDTO> GetTimezoneByLocation(Location location);
    }
}