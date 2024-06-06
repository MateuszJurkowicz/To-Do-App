using System.Text;
using System.Windows;
using System.Windows.Controls;
using To_Do_App.Controls;

namespace To_Do_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            sidebar.SelectedIndex = 1;
        }

        private void sidebar_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selected = sidebar.SelectedItem as NavButton;

            if (selected != null)
            {
                navframe.Navigate(selected.NavLink);
            }
        }
    }
}
