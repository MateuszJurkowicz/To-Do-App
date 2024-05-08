using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using To_Do_App.Helpers;
using To_Do_App.ViewModels.Base;
using To_Do_App.ViewModels.Controls;

namespace To_Do_App.ViewModels.Pages
{
    public class WorkTasksPageViewModel : BaseViewModel
    {
        public ObservableCollection<WorkTaskViewModel> WorkTasks { get; set; } = new ObservableCollection<WorkTaskViewModel>();
        public string NewWorkTaskTitle {  get; set; }
        public string NewWorkTaskDescription { get; set; }
        public ICommand AddNewTaskCommand { get; set; }
        public ICommand DeleteSelectedTasksCommand { get; set; }

        public WorkTasksPageViewModel()
        {
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
            WorkTasks.Add(newTask);
            NewWorkTaskTitle = string.Empty;
            NewWorkTaskDescription = string.Empty;

            OnPropertyChanged(nameof(NewWorkTaskTitle));
            OnPropertyChanged(nameof(NewWorkTaskDescription));
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
