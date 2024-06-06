using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_App.ViewModels.Pages;

namespace To_Do_App.ViewModels.Controls
{
    public class SubTaskViewModel : BaseViewModel
    {
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
