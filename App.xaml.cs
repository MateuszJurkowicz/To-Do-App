using System.Configuration;
using System.Data;
using System.Windows;
using To_Do_App.Data;
using To_Do_App.Helpers;

namespace To_Do_App
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var database = new ToDoAppDbContext();

            database.Database.EnsureCreated();

            DatabaseLocator.Database = database;
        }
    }

}
