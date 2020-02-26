using System;
using System.Collections.Generic;
using System.Linq;
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

        public async Task<CurrentWeatherDTO> GetCurrentWeatherByLocation(Location location)
        {
            var response = await this._client.GetAsync($"weather?lat={location.Latitude}&lon={location.Longitude}&units=metric");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<CurrentWeatherDTO>(responseStream);
            }

            throw new Exception(response.ReasonPhrase);
        }

        public async Task<CurrentWeatherDTO> GetCurrentWeatherByZipCode(string zipCode)
        {
            var response = await this._client.GetAsync($"weather?zip={zipCode}&units=metric");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<CurrentWeatherDTO>(responseStream);
            }

            throw new Exception(response.ReasonPhrase);
        }

        public async Task<CurrentWeatherDTO> GetCurrentWeatherByCity(string city)
        {
            var response = await this._client.GetAsync($"weather?q={city}&units=metric");
            if (response.IsSuccessStatusCode)
            {
                var responseStream = await response.Content.ReadAsStreamAsync();
                return await JsonSerializer.DeserializeAsync<CurrentWeatherDTO>(responseStream);
            }

            throw new Exception(response.ReasonPhrase);
        }

        public string GetReadableInfo(CurrentWeatherDTO weatherDto)
        {
            return $"📍 Location: {weatherDto.Name}\n" +
                    "🌡️ Current temperature: \n" +
                    $"{weatherDto.Main.Temperature}°C and it feels like {weatherDto.Main.FeelsLike}°C\n" +
                    $"Humidity: {weatherDto.Main.Humidity}%\n" +
                    "💨 Wind:\n" +
                    $"Speed - {weatherDto.Wind.Speed}km/h\n" +
                    $"Degree - {weatherDto.Wind.Degree}%\n" +
                    $"Pressure - {weatherDto.Main.Pressure}hpa\n" +
                    $"Primarily: {weatherDto.Weather.ElementAt(0).Main}\n";
        }

        public string GetIconUrl(CurrentWeatherDTO weatherDto)
        {
            return $"https://openweathermap.org/img/w/{weatherDto.Weather.ElementAt(0).Icon}.png";
        }
    }
}