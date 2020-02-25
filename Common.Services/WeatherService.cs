using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services;
using Telegram.Bot.Types;
using Utf8Json;

namespace Common.Services
{
    public class WeatherService : IWeatherService
    {
        private readonly HttpClient _client;

        public WeatherService(HttpClient client)
        {
            this._client = client;
        }
        public async Task<string> GetCurrentWeatherByLocation(Location location)
        {
            var response = await this._client.GetAsync($"weather?lat={location.Latitude}&lon={location.Longitude}&units=metric");
            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var responseStream = await response.Content.ReadAsStreamAsync();
                var weatherDto = await JsonSerializer.DeserializeAsync<CurrentWeatherDTO>(responseStream);
                return $"Current weather in {weatherDto.Name} is {weatherDto.Main.Temperature}°C and it feels like {weatherDto.Main.FeelsLike}°C";
            }

            throw new Exception(response.ReasonPhrase);
        }
    }
}