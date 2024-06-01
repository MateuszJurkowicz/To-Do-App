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
        public DateTime? NewWorkTaskFinishDate { get; set; }
        public bool NewWorkTaskIsImportant { get; set; } // Nowe pole

        public ICommand AddNewTaskCommand { get; set; }
        public ICommand EditTaskCommand { get; set; }
        public ICommand DeleteSelectedTasksCommand { get; set; }

        private WorkTaskViewModel _currentEditingTask;
        private readonly ToDoAppDbContext _context;

        public WorkTasksPageViewModel()
        {
            _context = new ToDoAppDbContext();
            LoadTasks();

            AddNewTaskCommand = new RelayCommand(AddNewTask);
            EditTaskCommand = new RelayCommand(EditTask);
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
                FinishDate = task.FinishDate,
                IsImportant = task.IsImportant, // Uwzględnij nowe pole
                IsSelected = false
            }).OrderByDescending(t => t.IsImportant).ThenBy(t => t.FinishDate)); // Sortowanie
        }

        public void LoadTaskToEdit(WorkTaskViewModel task)
        {
            _currentEditingTask = task;
            NewWorkTaskTitle = task.Title;
            NewWorkTaskDescription = task.Description;
            NewWorkTaskFinishDate = task.FinishDate;
            NewWorkTaskIsImportant = task.IsImportant;

            OnPropertyChanged(nameof(NewWorkTaskTitle));
            OnPropertyChanged(nameof(NewWorkTaskDescription));
            OnPropertyChanged(nameof(NewWorkTaskFinishDate));
            OnPropertyChanged(nameof(NewWorkTaskIsImportant));
        }

        private void AddNewTask()
        {
            var newTask = new WorkTaskViewModel
            {
                Title = NewWorkTaskTitle,
                Description = NewWorkTaskDescription,
                FinishDate = NewWorkTaskFinishDate,
                IsImportant = NewWorkTaskIsImportant // Nowe pole
            };

            var taskEntity = new WorkTask
            {
                Title = newTask.Title,
                Description = newTask.Description,
                FinishDate = newTask.FinishDate,
                IsImportant = newTask.IsImportant // Nowe pole
            };

            _context.WorkTasks.Add(taskEntity);
            _context.SaveChanges();

            newTask.Id = taskEntity.Id;
            WorkTasks.Add(newTask);
            SortTasks(); // Sortowanie po dodaniu nowego zadania

            NewWorkTaskTitle = string.Empty;
            NewWorkTaskDescription = string.Empty;
            NewWorkTaskFinishDate = null;
            NewWorkTaskIsImportant = false;

            OnPropertyChanged(nameof(NewWorkTaskTitle));
            OnPropertyChanged(nameof(NewWorkTaskDescription));
            OnPropertyChanged(nameof(NewWorkTaskFinishDate));
            OnPropertyChanged(nameof(NewWorkTaskIsImportant));
        }

        private void EditTask()
        {
            if (_currentEditingTask == null) return;

            _currentEditingTask.Title = NewWorkTaskTitle;
            _currentEditingTask.Description = NewWorkTaskDescription;
            _currentEditingTask.FinishDate = NewWorkTaskFinishDate;
            _currentEditingTask.IsImportant = NewWorkTaskIsImportant; // Nowe pole

            var taskEntity = _context.WorkTasks.Find(_currentEditingTask.Id);
            if (taskEntity != null)
            {
                taskEntity.Title = _currentEditingTask.Title;
                taskEntity.Description = _currentEditingTask.Description;
                taskEntity.FinishDate = _currentEditingTask.FinishDate;
                taskEntity.IsImportant = _currentEditingTask.IsImportant; // Nowe pole

                _context.SaveChanges();
            }

            _currentEditingTask = null;

            NewWorkTaskTitle = string.Empty;
            NewWorkTaskDescription = string.Empty;
            NewWorkTaskFinishDate = null;
            NewWorkTaskIsImportant = false;

            OnPropertyChanged(nameof(NewWorkTaskTitle));
            OnPropertyChanged(nameof(NewWorkTaskDescription));
            OnPropertyChanged(nameof(NewWorkTaskFinishDate));
            OnPropertyChanged(nameof(NewWorkTaskIsImportant));
            SortTasks(); // Sortowanie po edycji zadania
        }

        private void DeleteSelectedTasks()
        {
            var selectedTasks = WorkTasks.Where(t => t.IsSelected).ToList();

            foreach (var task in selectedTasks)
            {
                var taskEntity = _context.WorkTasks.Find(task.Id);
                if (taskEntity != null)
                {
                    _context.WorkTasks.Remove(taskEntity);
                }

                WorkTasks.Remove(task);
            }

            _context.SaveChanges();
        }

        private void SortTasks()
        {
            var sortedTasks = WorkTasks.OrderByDescending(t => t.IsImportant).ThenBy(t => t.FinishDate).ToList();
            WorkTasks.Clear();
            foreach (var task in sortedTasks)
            {
                WorkTasks.Add(task);
            }
        }
    }

}
