using System;
using System.Net.Http;
using System.Threading.Tasks;
using Common.DTO;
using Common.Services.Infrastructure.Services;
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
        public async Task<string> GetCurrentWeatherByLocation(double longitude, double latitude)
        {
            var response = await this._client.GetAsync($"weather?lat={latitude}&lon={longitude}&units=metric");
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