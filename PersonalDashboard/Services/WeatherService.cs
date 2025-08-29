using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using PersonalDashboard.Models;

namespace PersonalDashboard.Services
{
    /// <summary>
    /// Calls the Open-Meteo API by requesting current weather + next days min max, and weather codes
    /// </summary>
    public class WeatherService
    {
        private static readonly HttpClient _http = new HttpClient();

        public async Task<WeatherResponse> GetAsync(double lat, double lon, int days =4)
        {
            var url =
                $"https://api.open-meteo.com/v1/forecast" +
                $"?latitude={lat}&longitude={lon}" +
                $"&current=temperature_2m,wind_speed_10m,weather_code" +
                $"&daily=temperature_2m_max,temperature_2m_min,weather_code" +
                $"&forecast_days={days}" +
                $"&timezone=auto";

            //Make simple GET request
            var json = await _http.GetStringAsync(url);

            //Deserialize into our string model
            var result = JsonConvert.DeserializeObject<WeatherResponse>(json);

            return result;
        }
    }
}
