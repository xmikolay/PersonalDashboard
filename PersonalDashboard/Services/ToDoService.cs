using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using PersonalDashboard.Models;

namespace PersonalDashboard.Services
{
    /// <summary>
    /// Handles data operations for To-Do tasks, such as loading and saving tasks to a file.
    /// </summary>
    public class ToDoService
    {
        private readonly string _folderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "PersonalDashboard");
        private readonly string _filePath;

        public ToDoService()
        {
            _filePath = Path.Combine(_folderPath, "tasks.json");
        }

        public List<TaskItems> LoadTasks()
        {
            try
            {
                if (!File.Exists(_filePath))
                    return new List<TaskItems>();   //nothing saved yet

                var json = File.ReadAllText(_filePath);
                var tasks = JsonConvert.DeserializeObject<List<TaskItems>>(json);

                return tasks ?? new List<TaskItems>();
            }
            catch
            {
                //fail with empty list incase of fatal error with read or json
                return new List<TaskItems>();
            }
               
        }

        public void SaveTasks(IEnumerable<TaskItems> tasks)
        {
            try
            {
                if (!Directory.Exists(_folderPath))
                    Directory.CreateDirectory(_folderPath);

                var json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
                File.WriteAllText(_filePath, json);
            }
            catch
            {
                //fail silently for now
            }
        }
    }
}
