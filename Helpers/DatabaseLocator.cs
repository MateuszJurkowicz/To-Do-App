using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_App.Data;

namespace To_Do_App.Helpers
{
    public class DatabaseLocator
    {
        public static ToDoAppDbContext Database { get; set; }
    }
}
