using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PersonalDashboard.Models
{
    public class TaskItems : INotifyPropertyChanged
    {
        private string _description;
        private bool _isCompleted;

        //Task text that user inputs
        public string Description 
        { 
            get => _description ;
            set { _description = value; OnPropertyChanged(); } 
        }

        //Whether task is completed or not
        public bool isCompleted 
        { 
            get => _isCompleted; 
            set { _isCompleted = value; OnPropertyChanged(); }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
