using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.DTO
{
    public class CurrentWeatherDTO
    {
        [DataMember(Name = "coord")]
        public Coordinate Coordinates { get; set; }

        [DataMember(Name = "weather")]
        public IEnumerable<Weather> Weather { get; set; }

        [DataMember(Name = "base")]
        public string Base { get; set; }

        [DataMember(Name = "main")]
        public Main Main { get; set; }

        [DataMember(Name = "wind")]
        public Wind Wind { get; set; }

        [DataMember(Name = "clouds")]
        public Clouds Clouds { get; set; }

        [DataMember(Name = "dt")]
        public int Dt { get; set; }

        [DataMember(Name = "sys")]
        public System System { get; set; }

        [DataMember(Name = "timezone")]
        public int Timezone { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "cod")]
        public int StatusCode { get; set; }
    }

    public class Coordinate
    {
        [DataMember(Name = "lon")]
        public double Longitude { get; set; }

        [DataMember(Name = "lat")]
        public double Latitude { get; set; }
    }

    public class Weather
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "main")]
        public string Main { get; set; }

        [DataMember(Name = "description")]
        public string Description { get; set; }

        [DataMember(Name = "icon")]
        public string Icon { get; set; }
    }

    public class Main
    {
        [DataMember(Name = "temp")]
        public decimal Temperature { get; set; }

        [DataMember(Name = "feels_like")]
        public decimal FeelsLike { get; set; }

        [DataMember(Name = "temp_min")]
        public decimal TemperatureMin { get; set; }

        [DataMember(Name = "temp_max")]
        public decimal TemperatureMax { get; set; }

        [DataMember(Name = "pressure")]
        public int Pressure { get; set; }

        [DataMember(Name = "humidity")]
        public int Humidity { get; set; }
    }

    public class Wind
    {
        [DataMember(Name = "speed")]
        public double Speed { get; set; }

        [DataMember(Name = "deg")]
        public double Degree { get; set; }
    }

    public class Clouds
    {
        [DataMember(Name = "all")]
        public int All { get; set; }
    }

    public class System
    {
        [DataMember(Name = "type")]
        public int Type { get; set; }

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "message")]
        public double Message { get; set; }

        [DataMember(Name = "country")]
        public string Country { get; set; }

        [DataMember(Name = "sunrise")]
        public int Sunrise { get; set; }

        [DataMember(Name = "sunset")]
        public int Sunset { get; set; }
    }
}