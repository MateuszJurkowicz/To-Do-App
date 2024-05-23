using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using To_Do_App.ViewModels;
using To_Do_App.ViewModels.Controls;
using To_Do_App.ViewModels.Pages;

namespace To_Do_App.Controls
{
    public partial class WorkTask : UserControl
    {
        public WorkTask()
        {
            InitializeComponent();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Hand;
            var storyboard = (Storyboard)this.Resources["MouseEnterStoryboard"];
            storyboard.Begin();
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
            var storyboard = (Storyboard)this.Resources["MouseLeaveStoryboard"];
            storyboard.Begin();
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (DataContext is WorkTaskViewModel viewModel)
            {
                // Find the parent WorkTasksPage
                var parentPage = FindParent<Page>(this);
                if (parentPage != null && parentPage.DataContext is WorkTasksPageViewModel mainViewModel)
                {
                    mainViewModel.LoadTaskToEdit(viewModel);
                }
            }
        }

        private T FindParent<T>(DependencyObject child) where T : DependencyObject
        {
            DependencyObject parentObject = VisualTreeHelper.GetParent(child);
            if (parentObject == null) return null;

            if (parentObject is T parent)
            {
                return parent;
            }
            else
            {
                return FindParent<T>(parentObject);
            }
        }
    }
}
