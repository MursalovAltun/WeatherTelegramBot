using System.Text.Json;
using Common.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Common.WebApiCore.Controllers
{
    /// <summary>
    /// A controller is using for authorize user
    /// </summary>
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            var b =
                "{\"coord\":{\"lon\":-11.41,\"lat\":20.66},\"weather\":[{\"id\":804,\"main\":\"Clouds\",\"description\":\"overcast clouds\",\"icon\":\"04d\"}],\"base\":\"stations\",\"main\":{\"temp\":308.57,\"feels_like\":301,\"temp_min\":308.57,\"temp_max\":308.57,\"pressure\":1012,\"humidity\":8,\"sea_level\":1012,\"grnd_level\":967},\"wind\":{\"speed\":7.26,\"deg\":152},\"clouds\":{\"all\":100},\"dt\":1582472130,\"sys\":{\"country\":\"MR\",\"sunrise\":1582441855,\"sunset\":1582483656},\"timezone\":0,\"id\":2380519,\"name\":\"Chinguetti\",\"cod\":200}";
            var a = JsonSerializer.Deserialize<CurrentWeatherDTO>(b);
            return Ok(a);
        }
    }
}