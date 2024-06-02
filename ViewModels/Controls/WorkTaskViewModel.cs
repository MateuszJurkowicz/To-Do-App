using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_App.ViewModels.Pages;


namespace To_Do_App.ViewModels.Controls
{
    public class WorkTaskViewModel : BaseViewModel
    {
        public int Id { get; set; }
        private string _title;
        public string Title
        {
            get => IsImportant ? $"Ważne! {_title}" : _title;
            set
            {
                _title = value;
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Description { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsSelected { get; set; }
        private bool _isImportant;
        public bool IsImportant
        {
            get => _isImportant;
            set
            {
                _isImportant = value;
                OnPropertyChanged(nameof(IsImportant));
                OnPropertyChanged(nameof(Title));
            }
        }
        public string Category { get; set; }
    }
}
