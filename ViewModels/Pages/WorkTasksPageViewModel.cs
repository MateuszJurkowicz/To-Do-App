using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using To_Do_App.Data;
using To_Do_App.Helpers;
using To_Do_App.ViewModels.Base;
using To_Do_App.ViewModels.Controls;

namespace To_Do_App.ViewModels.Pages
{
    public class WorkTasksPageViewModel : BaseViewModel
    {
        public ObservableCollection<WorkTaskViewModel> WorkTasks { get; set; }
        public string NewWorkTaskTitle {  get; set; }
        public string NewWorkTaskDescription { get; set; }
        public ICommand AddNewTaskCommand { get; set; }
        public ICommand DeleteSelectedTasksCommand { get; set; }

        public WorkTasksPageViewModel()
        {
            using (var db = new ToDoAppDb())
            {
                WorkTasks = new ObservableCollection<WorkTaskViewModel>(db.WorkTasks.ToList());
            }
            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
        }
        private void AddNewTask() 
        {
            var newTask = new WorkTaskViewModel
            {
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
                CreatedDate = DateTime.Now
            };
            using (var db = new ToDoAppDb())
            {
                db.WorkTasks.Add(newTask);
                db.SaveChanges();
            }
            NewWorkTaskTitle = string.Empty;
            NewWorkTaskDescription = string.Empty;

        }

        private void DeleteSelectedTasks()
        {
            var selectedTasks = WorkTasks.Where(x => x.IsSelected).ToList();
            foreach (var task in selectedTasks)
            {
                WorkTasks.Remove(task);
            }
        }

    }
}
