using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using To_Do_App.Data;
using To_Do_App.Data.Entities;
using To_Do_App.Helpers;
using To_Do_App.ViewModels.Base;
using To_Do_App.ViewModels.Controls;

namespace To_Do_App.ViewModels.Pages
{
    public class WorkTasksPageViewModel : BaseViewModel
    {
        public ObservableCollection<WorkTaskViewModel> WorkTasks { get; set; }
        public string NewWorkTaskTitle { get; set; }
        public string NewWorkTaskDescription { get; set; }
        public DateTime? NewWorkTaskFinishDate { get; set; } // Zmiana na DateTime
        public ICommand AddNewTaskCommand { get; set; }
        public ICommand DeleteSelectedTasksCommand { get; set; }

        private readonly ToDoAppDbContext _context;

        public WorkTasksPageViewModel()
        {
            _context = new ToDoAppDbContext();
            LoadTasks();

            AddNewTaskCommand = new RelayCommand(AddNewTask);
            DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
        }

        private void LoadTasks()
        {
            var tasks = _context.WorkTasks.ToList();
            WorkTasks = new ObservableCollection<WorkTaskViewModel>(tasks.Select(task => new WorkTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                FinishDate = task.FinishDate.HasValue ? task.FinishDate.Value : (DateTime?)null,
                IsSelected = false
            }));
        }

        private void AddNewTask()
        {
            var newTask = new WorkTaskViewModel
            {
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
                FinishDate = NewWorkTaskFinishDate
            };

            // Dodaj nowy task do bazy danych
            var taskEntity = new WorkTask
            {
                Title = newTask.Title,
                Description = newTask.Description,
                FinishDate = newTask.FinishDate
            };

            _context.WorkTasks.Add(taskEntity);
            _context.SaveChanges();

            // Dodaj nowy task do kolekcji w UI
            newTask.Id = taskEntity.Id;
            WorkTasks.Add(newTask);

            // Wyczyść pola
            NewWorkTaskTitle = string.Empty;
            NewWorkTaskDescription = string.Empty;
            NewWorkTaskFinishDate = null;

            OnPropertyChanged(nameof(NewWorkTaskTitle));
            OnPropertyChanged(nameof(NewWorkTaskDescription));
            OnPropertyChanged(nameof(NewWorkTaskFinishDate));
        }

        private void DeleteSelectedTasks()
        {
            var selectedTasks = WorkTasks.Where(t => t.IsSelected).ToList();

            foreach (var task in selectedTasks)
            {
                // Usuń z bazy danych
                var taskEntity = _context.WorkTasks.Find(task.Id);
                if (taskEntity != null)
                {
                    _context.WorkTasks.Remove(taskEntity);
                }

                // Usuń z kolekcji w UI
                WorkTasks.Remove(task);
            }

            _context.SaveChanges();
        }
    }
}
