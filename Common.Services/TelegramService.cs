using System.Net.Http;
using System.Security.Authentication;
using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services;
using Utf8Json;

namespace Common.Services
{
    public class TelegramService : ITelegramService
    {
        private readonly HttpClient _client;

        public TelegramService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<UserDTO> GetMeAsync()
        {
            var response = await this._client.GetAsync("getMe");
            if (response.IsSuccessStatusCode)
            {
                var contentStream = await response.Content.ReadAsStreamAsync();
                var result = await JsonSerializer.DeserializeAsync<RootUser>(contentStream);
                return result.Result;
            }

            throw new AuthenticationException("Could not authorize, please check telegram bot token");
        }
    }
}