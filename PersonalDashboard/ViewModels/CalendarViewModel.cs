using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Input;
using PersonalDashboard.Models;
using PersonalDashboard.Helpers;

namespace PersonalDashboard.ViewModels
{
    /// <summary>
    /// Viewmodel for our calendar widget. Tracks visible month and selected date, holds a list of events for selected day.
    /// </summary>
    public class CalendarViewModel : MainViewModel
    {
        //The date the user has selected
        private DateTime _selectedDate;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set { _selectedDate = value; OnPropertyChanged(); RefreshEventsForSelectedDate(); }
        }

        //All events
        public ObservableCollection<EventItem> AllEvents { get;} = new ObservableCollection<EventItem>();

        //Only events that match SelectedDate
        public ObservableCollection<EventItem> EventsForSelectedDate { get; } = new ObservableCollection<EventItem>();

        private bool _hasEvents;
        public bool HasEvents
        {
            get => _hasEvents;
            set { _hasEvents = value; OnPropertyChanged(); }
        }

        public CalendarViewModel()
        {
            //Start today
            SelectedDate = DateTime.Today;

            //Seeding with some example events
            SeedSampleEvents();

            RefreshEventsForSelectedDate();
        }

        private void RefreshEventsForSelectedDate()
        {
            EventsForSelectedDate.Clear();

            var matches = AllEvents
                .Where(e => e.Date.Date == SelectedDate.Date)
                .OrderBy(e => e.Title); //No time field yet just sort by title

            foreach (var e in matches)
            {
                EventsForSelectedDate.Add(e);
            }

            HasEvents = EventsForSelectedDate.Count > 0;
        }

        //Our sample data
        private void SeedSampleEvents()
        {
            var t = DateTime.Today;
            AllEvents.Add(new EventItem { Date = t, Title = "Daily review" });
            AllEvents.Add(new EventItem { Date = t.AddDays(1), Title = "Gym session" });
            AllEvents.Add(new EventItem { Date = t.AddDays(2), Title = "Project milestone" });
            AllEvents.Add(new EventItem { Date = new DateTime(t.Year, t.Month, 1), Title = "Rent due" });
            AllEvents.Add(new EventItem { Date = new DateTime(t.Year, t.Month, 15), Title = "Payday" });
        }
    }
}
