using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Input;
using To_Do_App.Data;
using To_Do_App.Data.Entities;
using To_Do_App.Helpers;
using To_Do_App.ViewModels.Controls;
using To_Do_App.ViewModels.Pages;
public class WorkTasksPageViewModel : BaseViewModel
{
    public ObservableCollection<WorkTaskViewModel> WorkTasks { get; set; }

    private string _newWorkTaskTitle;

    [Required(ErrorMessage = "Tytuł nie może być pusty!")]
    [MinLength(5, ErrorMessage = "Tytuł musi mieć minimum 5 znaków!")]
    public string NewWorkTaskTitle
    {
        get { return _newWorkTaskTitle; }
        set
        {
            _newWorkTaskTitle = value;
            Validate(nameof(NewWorkTaskTitle), value);
            AddNewTaskCommand.RaiseCanExecuteChanged();
            EditTaskCommand.RaiseCanExecuteChanged();
        }
    }

    private string _newWorkTaskDescription;

    [Required(ErrorMessage = "Opis nie może być pusty!")]
    [MaxLength(200, ErrorMessage = "Opis nie może mieć więcej niż 200 znaków!")]
    public string NewWorkTaskDescription
    {
        get { return _newWorkTaskDescription; }
        set
        {
            _newWorkTaskDescription = value;
            Validate(nameof(NewWorkTaskDescription), value);
            AddNewTaskCommand.RaiseCanExecuteChanged();
            EditTaskCommand.RaiseCanExecuteChanged();
        }
    }

    private DateTime? _newWorkTaskFinishDate;

    [CustomValidation(typeof(WorkTasksPageViewModel), nameof(ValidateFinishDate))]
    public DateTime? NewWorkTaskFinishDate
    {
        get { return _newWorkTaskFinishDate; }
        set
        {
            _newWorkTaskFinishDate = value;
            Validate(nameof(NewWorkTaskFinishDate), value);
            AddNewTaskCommand.RaiseCanExecuteChanged();
            EditTaskCommand.RaiseCanExecuteChanged();
        }
    }

    public bool NewWorkTaskIsImportant { get; set; }
    public string NewWorkTaskCategory { get; set; }
    public ActionCommand AddNewTaskCommand { get; set; }
    public ActionCommand EditTaskCommand { get; set; }
    public ICommand DeleteSelectedTasksCommand { get; set; }

    private WorkTaskViewModel _currentEditingTask;
    private readonly ToDoAppDbContext _context;
    public ObservableCollection<string> Categories { get; set; } = new ObservableCollection<string> { "Praca", "Szkoła", "Dom", "Inne" };

    public WorkTasksPageViewModel()
    {
        _context = new ToDoAppDbContext();
        LoadTasks();

        AddNewTaskCommand = new ActionCommand(Submit, CanSubmit);
        EditTaskCommand = new ActionCommand(Edit, CanEdit);
        DeleteSelectedTasksCommand = new RelayCommand(DeleteSelectedTasks);
        NewWorkTaskCategory = Categories[3];
    }

    public static ValidationResult ValidateFinishDate(DateTime? finishDate, ValidationContext context)
    {
        if (finishDate.HasValue && finishDate.Value <= DateTime.Now)
        {
            return new ValidationResult("Data realizacji już minęła!");
        }

        return ValidationResult.Success;
    }

    private bool CanEdit(object obj)
    {
        return Validator.TryValidateObject(this, new ValidationContext(this), null);
    }

    private void Edit(object obj)
    {
        EditTask();
    }

    private bool CanSubmit(object obj)
    {
        return Validator.TryValidateObject(this, new ValidationContext(this), null);
    }

    private void Submit(object obj)
    {
        AddNewTask();
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
            IsImportant = task.IsImportant,
            Category = task.Category,
            IsSelected = false
        }).OrderByDescending(t => t.IsImportant).ThenBy(t => t.FinishDate));
        OnPropertyChanged(nameof(WorkTasks));
    }


    public void LoadTaskToEdit(WorkTaskViewModel task)
    {
        _currentEditingTask = task;
        NewWorkTaskTitle = task.Title;
        NewWorkTaskDescription = task.Description;
        NewWorkTaskFinishDate = task.FinishDate;
        NewWorkTaskIsImportant = task.IsImportant;
        NewWorkTaskCategory = task.Category; 

        OnPropertyChanged(nameof(NewWorkTaskTitle));
        OnPropertyChanged(nameof(NewWorkTaskDescription));
        OnPropertyChanged(nameof(NewWorkTaskFinishDate));
        OnPropertyChanged(nameof(NewWorkTaskIsImportant));
        OnPropertyChanged(nameof(NewWorkTaskCategory));  
    }

    private void AddNewTask()
    {
        var newTask = new WorkTaskViewModel
        {
            Title = NewWorkTaskTitle,
            Description = NewWorkTaskDescription,
            FinishDate = NewWorkTaskFinishDate,
            IsImportant = NewWorkTaskIsImportant,
            Category = NewWorkTaskCategory  
        };

        var taskEntity = new WorkTask
        {
            Title = newTask.Title,
            Description = newTask.Description,
            FinishDate = newTask.FinishDate,
            IsImportant = newTask.IsImportant,
            Category = newTask.Category  
        };

        _context.WorkTasks.Add(taskEntity);
        _context.SaveChanges();

        newTask.Id = taskEntity.Id;
        WorkTasks.Add(newTask);
        SortTasks();

        NewWorkTaskTitle = string.Empty;
        NewWorkTaskDescription = string.Empty;
        NewWorkTaskFinishDate = null;
        NewWorkTaskIsImportant = false;
        NewWorkTaskCategory = Categories[3];  

        OnPropertyChanged(nameof(NewWorkTaskTitle));
        OnPropertyChanged(nameof(NewWorkTaskDescription));
        OnPropertyChanged(nameof(NewWorkTaskFinishDate));
        OnPropertyChanged(nameof(NewWorkTaskIsImportant));
        OnPropertyChanged(nameof(NewWorkTaskCategory));  
    }

    private void EditTask()
    {
        if (_currentEditingTask == null) return;

        _currentEditingTask.Title = NewWorkTaskTitle;
        _currentEditingTask.Description = NewWorkTaskDescription;
        _currentEditingTask.FinishDate = NewWorkTaskFinishDate;
        _currentEditingTask.IsImportant = NewWorkTaskIsImportant;
        _currentEditingTask.Category = NewWorkTaskCategory; 

        var taskEntity = _context.WorkTasks.Find(_currentEditingTask.Id);
        if (taskEntity != null)
        {
            taskEntity.Title = _currentEditingTask.Title;
            taskEntity.Description = _currentEditingTask.Description;
            taskEntity.FinishDate = _currentEditingTask.FinishDate;
            taskEntity.IsImportant = _currentEditingTask.IsImportant;
            taskEntity.Category = _currentEditingTask.Category;  

            _context.SaveChanges();
        }

        _currentEditingTask = null;

        NewWorkTaskTitle = string.Empty;
        NewWorkTaskDescription = string.Empty;
        NewWorkTaskFinishDate = null;
        NewWorkTaskIsImportant = false;
        NewWorkTaskCategory = null;  

        OnPropertyChanged(nameof(NewWorkTaskTitle));
        OnPropertyChanged(nameof(NewWorkTaskDescription));
        OnPropertyChanged(nameof(NewWorkTaskFinishDate));
        OnPropertyChanged(nameof(NewWorkTaskIsImportant));
        OnPropertyChanged(nameof(NewWorkTaskCategory));  
        SortTasks();
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
