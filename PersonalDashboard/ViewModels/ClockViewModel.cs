using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using PersonalDashboard.ViewModels;

namespace PersonalDashboard.ViewModels
{
    /// <summary>
    /// Viewmodel for our clock widget, keeps current time and current date updated every second.
    /// </summary>
    public class ClockViewModel : MainViewModel
    {
        private string _currentTime;
        public string CurrentTime
        {
            get => _currentTime; 
            set
            {
                _currentTime = value;
                OnPropertyChanged();
            }
        }

        private string _currentDate;
        public string CurrentDate
        {
            get => _currentDate; 
            set
            {
                _currentDate = value;
                OnPropertyChanged();
            }
        }

        //DispatcherTimer to update time every second
        private readonly DispatcherTimer _timer;

        public ClockViewModel()
        {
            // Initialize values first so the UI shows data before first tick
            UpdateNow();

            // Create a timer that ticks every second
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };

            _timer.Tick += (_, __) => UpdateNow(); //On each tick, update the time and date
            _timer.Start(); //Start the timer
        }

        private void UpdateNow()
        {
            CurrentTime = DateTime.Now.ToString("HH:mm:ss");
            CurrentDate = DateTime.Now.ToString("ddd, dd MMM yyy");
        }
    }
}
