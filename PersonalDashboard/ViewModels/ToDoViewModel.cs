using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PersonalDashboard.Helpers;
using System.Collections.ObjectModel;
using PersonalDashboard.Models;
using System.Collections.Specialized;
using System.ComponentModel;
using PersonalDashboard.Services;

namespace PersonalDashboard.ViewModels
{
    /// <summary>
    /// Viewmodel for managing the to-do widget.
    /// - Holds the list of tasks.
    /// - Handles adding and removing tasks.
    /// - Persists tasks via ToDoService
    /// </summary>
    public class ToDoViewModel : MainViewModel
    {
        private readonly ToDoService _service = new ToDoService();

        //Collection of tasks displayed in the UI (bound to listbox in xaml)
        public ObservableCollection<TaskItems> Tasks { get; set; } = new ObservableCollection<TaskItems>();

        private string _newTaskDescription;
        public string NewTaskDescription 
        { 
            get => _newTaskDescription; 
            set { _newTaskDescription = value; OnPropertyChanged(); }
        }

        //Commands bound to the add and remove buttons in the UI
        public ICommand AddTaskCommand { get; set; }
        public ICommand RemoveTaskCommand { get; set; }

        public ToDoViewModel()
        {
            //Initialize commands, add button is only enabled if there is text in the input box
            AddTaskCommand = new RelayCommand(_ => AddTask(), _ => !string.IsNullOrWhiteSpace(NewTaskDescription));
            RemoveTaskCommand = new RelayCommand(task => RemoveTask(task as TaskItems));

            //Load saved tasks from file
            var loaded = _service.LoadTasks();
            foreach (var t in loaded)
            {
                Tasks.Add(t);
                SubscribeToItem(t); //watch for changes so we can auto-save
            }

            //If tasks are added or removed from collection, auto-save
            Tasks.CollectionChanged += Tasks_CollectionChanged;
        }

        //Add a new task from the input box
        private void AddTask()
        {
            if (string.IsNullOrWhiteSpace(NewTaskDescription))
            {
                return;
            }

            var item = new TaskItems 
            { 
                Description = NewTaskDescription, 
                isCompleted = false 
            };

            Tasks.Add(item);
            SubscribeToItem(item); //save when item changes
            NewTaskDescription = string.Empty; //clear input box

            SaveNow();
        }

        //Remove task when button pressed
        private void RemoveTask(TaskItems task)
        {
            if (task == null)
            {
                return;
            }

            UnsubscribeFromItem(task); //stop watching for changes
            Tasks.Remove(task);
            SaveNow();
        }

        //Save whenever the collection changes (item added or removed)
        private void Tasks_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (TaskItems item in e.NewItems)
                {
                    SubscribeToItem(item);
                }
            }

            if (e.OldItems != null)
            {
                foreach (TaskItems item in e.OldItems)
                {
                    UnsubscribeFromItem(item);
                }
            }
            SaveNow();
        }

        //Watch each items property changes, for example IsCompleted toggled
        private void SubscribeToItem(TaskItems item)
        {
            if (item == null)
            {
                return;                
            }
            item.PropertyChanged += Task_PropertyChanged;
        }

        private void UnsubscribeFromItem(TaskItems item)
        {
            if (item == null)
            {
                return;
            }
            item.PropertyChanged -= Task_PropertyChanged;
        }

        //When a tasks description or completed status changes, save
        private void Task_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            SaveNow();
        }

        private void SaveNow()
        {
            _service.SaveTasks(Tasks);
        }
    }
}
