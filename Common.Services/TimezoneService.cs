using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services;
using Telegram.Bot.Types;
using Utf8Json;

namespace Common.Services
{
    public class TimezoneService : ITimezoneService
    {
        private readonly HttpClient _client;

        public TimezoneService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<TimezoneDTO> GetTimezoneByLocation(Location location)
        {
            var response =
                await this._client.GetAsync($"get-time-zone?by=position&lat={location.Latitude}&lng={location.Longitude}");

            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<TimezoneDTO>(responseStream);
            }

            throw new Exception(response.ReasonPhrase);
        }
    }
}