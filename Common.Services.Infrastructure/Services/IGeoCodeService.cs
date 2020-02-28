using System.Threading.Tasks;
using Common.DTO;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.Services
{
    public interface IGeoCodeService
    {
        Task<GeoCodeDTO> GetCityByLocation(Location location);
    }
}