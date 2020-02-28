using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services;
using Telegram.Bot.Types;
using Utf8Json;

namespace Common.Services
{
    /// <summary>
    /// Documentation <see href="https://geocode.xyz/api"></see>
    /// </summary>
    public class GeoCodeService : IGeoCodeService
    {
        private readonly HttpClient _client;

        public GeoCodeService(HttpClient client)
        {
            this._client = client;
        }

        public async Task<GeoCodeDTO> GetCityByLocation(Location location)
        {
            var response = await this._client.GetAsync($"{location.Latitude},{location.Longitude}");
            
            if(response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<GeoCodeDTO>(responseStream);
            }

            throw new Exception(response.ReasonPhrase);
        }
    }
}