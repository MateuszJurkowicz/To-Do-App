﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_App.ViewModels.Pages;


namespace To_Do_App.ViewModels.Controls
{
    public class WorkTaskViewModel : BaseViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsSelected { get; set; }
        public bool IsImportant { get; set; }
        public string Category { get; set; }
        public ObservableCollection<SubTaskViewModel> SubTasks { get; set; } = new ObservableCollection<SubTaskViewModel>();

    }
}
