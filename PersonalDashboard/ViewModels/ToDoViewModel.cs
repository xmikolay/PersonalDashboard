using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PersonalDashboard.Helpers;
using System.Collections.ObjectModel;
using PersonalDashboard.Models;

namespace PersonalDashboard.ViewModels
{
    public class ToDoViewModel : MainViewModel
    {
        public ObservableCollection<TaskItems> Tasks { get; set; } = new ObservableCollection<TaskItems>();
        public string NewTaskDescription { get; set; }

        public ICommand AddTaskCommand { get; set; }
        public ICommand RemoveTaskCommand { get; set; }

        public ToDoViewModel()
        {
            AddTaskCommand = new RelayCommand(_ => AddTask(), _ => !string.IsNullOrWhiteSpace(NewTaskDescription));
            RemoveTaskCommand = new RelayCommand(task => RemoveTask(task as TaskItems));
        }

        private void AddTask()
        {
            if (!string.IsNullOrWhiteSpace(NewTaskDescription))
            {
                Tasks.Add(new TaskItems { Description = NewTaskDescription, isCompleted = false });
                NewTaskDescription = string.Empty; // Clear the input after adding
                OnPropertyChanged(nameof(NewTaskDescription));
            }
        }

        private void RemoveTask(TaskItems task)
        {
            if (task != null)
            {
                Tasks.Remove(task);
            }
        }
    }
}
