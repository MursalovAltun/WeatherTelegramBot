﻿using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace Common.Services.Infrastructure.Services
{
    public interface IWeatherService
    {
        Task<string> GetCurrentWeatherByLocation(Location location);
    }
}