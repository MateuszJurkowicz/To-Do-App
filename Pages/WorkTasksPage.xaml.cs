using System.Windows.Controls;
using To_Do_App.ViewModels.Pages;

namespace To_Do_App.Pages
{
    /// <summary>
    /// Interaction logic for WorkTasksPage.xaml
    /// </summary>
    public partial class WorkTasksPage : Page
    {
        public WorkTasksPage()
        {
            InitializeComponent();

            DataContext = new WorkTasksPageViewModel();
        }
    }
}
