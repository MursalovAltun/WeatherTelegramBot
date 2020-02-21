using System.Threading.Tasks;
using Common.DTO;

namespace Common.Services.Infrastructure.Services
{
    public interface ITelegramService
    {
        Task<UserDTO> GetMeAsync();
    }
}