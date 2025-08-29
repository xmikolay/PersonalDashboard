using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDashboard.Models
{
    /// <summary>
    /// Strongly typed model for weather data for a small subset of Open-Meteo API response
    /// </summary>
    public class WeatherCurrent
    {
        public double temperature_2m { get; set; }
        public double wind_speed_10m { get; set; }
        public int weather_code { get; set; }
    }

    public class WeatherDaily
    {
        public List<DateTime> time { get; set; }
        public List<double> temperature_2m_max { get; set; }
        public List<double> temperature_2m_min { get; set; }
        public List<int> weather_code { get; set; }
    }

    public class WeatherResponse
    {
        public WeatherCurrent current { get; set; }
        public WeatherDaily daily { get; set; }
        public string timezone { get; set; }
    }

    public class ForecastDay
    { 
        public DateTime Date { get; set; }
        public double Max { get; set; }
        public double Min { get; set; }
        public int WeatherCode { get; set; }
        public string Summary { get; set; }
    }

    public static class WeatherCodeHelper
    {
        //Mapping WMO codes to text

        public static string ToText(int code)
        {
            switch (code)
            {
                case 0: return "Clear";
                case 1: return "Partly cloudy";
                case 2: return "Partly cloudy";
                case 3: return "Cloudy";
                case 45:
                case 48: return "Fog";
                case 51:
                case 53:
                case 55: return "Drizzle";
                case 61:
                case 63:
                case 65: return "Rain";
                case 66:
                case 67: return "Freezing rain";
                case 71:
                case 73:
                case 75: return "Snowfall";
                case 77: return "Snow grains";
                case 80:
                case 81:
                case 82: return "Rain showers";
                case 85:
                case 86: return "Snow showers";
                case 95: return "Thunderstorm";
                case 96:
                case 99: return "Thunderstorm & hail";
                default: return "—";
            }           
        }
    }
}
