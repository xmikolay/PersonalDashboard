using PersonalDashboard.Models;
using PersonalDashboard.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalDashboard.ViewModels
{
    /// <summary>
    /// Viewmodel for weather widget. Shows basic info. Coordinates are hardcoded for now.
    /// </summary>
    public class WeatherViewModel : MainViewModel
    {
        private readonly WeatherService _service = new WeatherService();

        //Bindable properties for the UI
        private string _locationLabel = "Your Location";
        public string LocationLabel
        {
            get => _locationLabel;
            set { _locationLabel = value; OnPropertyChanged(); }
        }

        private string _currentTemp;
        public string CurrentTemp
        {
            get => _currentTemp;
            set { _currentTemp = value; OnPropertyChanged(); }
        }

        private string _currentWind;
        public string CurrentWind
        {
            get => _currentWind;
            set { _currentWind = value; OnPropertyChanged(); }
        }

        private string _currentSummary;
        public string CurrentSummary
        {
            get => _currentSummary;
            set { _currentSummary = value; OnPropertyChanged(); }
        }

        //Next days excluding today
        public ObservableCollection<ForecastDay> NextDays { get; } = new ObservableCollection<ForecastDay>();

        public WeatherViewModel()
        {
            _ = LoadAsync();
        }

        //Fetches weather and updates bindable properties
        private async Task LoadAsync()
        {
            try
            {
                double lat = 54.278038;
                double lon = -8.460736;

                //Ask for 4 days in total (today + 3 next)
                var data = await _service.GetAsync(lat, lon, days: 4);

                //Get current conditons
                var t = data.current.temperature_2m;
                var w = data.current.wind_speed_10m;
                var code = data.current.weather_code;

                CurrentTemp = $"{Math.Round(t)}C";
                CurrentWind = $"{Math.Round(w)} km/h";
                CurrentSummary = WeatherCodeHelper.ToText(code);

                //Build next 3 days
                NextDays.Clear();
                for(int i = 1; i < data.daily.time.Count && i <= 3; i++)
                {
                    var d = new ForecastDay
                    {
                        Date = data.daily.time[i],
                        Min = data.daily.temperature_2m_min[i],
                        Max = data.daily.temperature_2m_max[i],
                        WeatherCode = data.daily.weather_code[i],
                        Summary = WeatherCodeHelper.ToText(data.daily.weather_code[i])
                    };
                    NextDays.Add(d);
                }
            }
            catch(Exception ex)
            {
                CurrentTemp = "-";
                CurrentWind = "-";
                CurrentSummary = "Weather Unavailable";
                NextDays.Clear();
            }
        }
    }
}
