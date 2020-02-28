using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.Services
{
    public interface ICommandService
    {
        Task HandleStart(Message message);

        Task HandleStop(Message message);

        Task HandleCurrentWeatherInfoByLocation(Message message);

        Task HandleCurrentWeatherInfoByZipCode(Message message);

        Task HandleCurrentWeatherInfoByZipCodeAnswer(Message message);

        Task HandleCurrentWeatherInfoByCity(Message message);

        Task HandleCurrentWeatherInfoByCityAnswer(Message message);

        Task HandleSetCity(Message message);

        Task HandleSetCityAnswer(Message message);

        Task HandleGetLocation(Message message);

        Task HandleTerminate(Message message);

        Task HandleUnknown(Message message);
    }
}