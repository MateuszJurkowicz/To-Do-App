using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.Linq;
using To_Do_App.Data;
using To_Do_App.ViewModels.Controls;
using To_Do_App.ViewModels.Pages;

public class DetailedWorkTasksPageViewModel : BaseViewModel
{
    public ObservableCollection<WorkTaskViewModel> WorkTasks { get; set; }

    private readonly ToDoAppDbContext _context;

    public DetailedWorkTasksPageViewModel()
    {
        _context = new ToDoAppDbContext();
        LoadTasks();
    }

    private void LoadTasks()
    {
        var tasks = _context.WorkTasks.Include(t => t.SubTasks).ToList();
        WorkTasks = new ObservableCollection<WorkTaskViewModel>(
            tasks.Select(task => new WorkTaskViewModel
            {
                Id = task.Id,
                Title = task.Title,
                Description = task.Description,
                FinishDate = task.FinishDate,
                IsImportant = task.IsImportant,
                Category = task.Category,
                IsSelected = false,
                SubTasks = new ObservableCollection<SubTaskViewModel>(
                    task.SubTasks.Select(st => new SubTaskViewModel
                    {
                        Title = st.Title,
                        IsCompleted = st.IsCompleted
                    })
                )
            })
        );

        SortTasks();
        OnPropertyChanged(nameof(WorkTasks));
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
