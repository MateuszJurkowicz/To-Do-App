using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_App.ViewModels.Controls;

namespace To_Do_App.Data
{
    public class ToDoAppDb : DbContext
    {
        public DbSet<WorkTaskViewModel> WorkTasks { get; set; }
    }
}
