using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using To_Do_App.ViewModels;
using To_Do_App.ViewModels.Controls;

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
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var viewModel = DataContext as WorkTaskViewModel;
            if (viewModel != null)
            {
                viewModel.IsSelected = !viewModel.IsSelected;
            }
        }
    }
}
