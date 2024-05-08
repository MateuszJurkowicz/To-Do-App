using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_App.ViewModels.Controls;

namespace To_Do_App.ViewModels.Pages
{
    class WorkTasksPageViewModel
    {
        public List<WorkTaskViewModel> WorkTasks { get; set; } = new List<WorkTaskViewModel>();
        public string NewWorkTaskTitle {  get; set; }
        public string NewWorkTaskDescription { get; set; }
        private void AddNewTask() 
        {
            var newTask = new WorkTaskViewModel
            {
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
                CreatedDate = DateTime.Now
            };
            WorkTasks.Add(newTask);
        }

    }
}
