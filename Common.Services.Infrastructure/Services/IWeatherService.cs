using System.Threading.Tasks;
using Common.DTO;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.Services
{
    public interface IWeatherService
    {
        Task<CurrentWeatherDTO> GetCurrentWeatherByLocation(Location location);

        Task<CurrentWeatherDTO> GetCurrentWeatherByZipCode(string zipCode);

        Task<CurrentWeatherDTO> GetCurrentWeatherByCity(string city);

        string GetReadableInfo(CurrentWeatherDTO weatherDto);

        string GetIconUrl(CurrentWeatherDTO weatherDto);
    }
}