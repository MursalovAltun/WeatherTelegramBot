using System.Threading.Tasks;

namespace Common.Services.Infrastructure.Services
{
    public interface IWeatherService
    {
        Task<string> GetCurrentWeatherByLocation(double longitude, double latitude);
    }
}