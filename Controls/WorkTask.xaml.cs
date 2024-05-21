using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

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
    }
}
