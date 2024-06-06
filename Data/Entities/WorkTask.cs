using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_App.Data.Entities
{
    public class WorkTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime? FinishDate { get; set; }
        public bool IsImportant { get; set; }
        public string Category { get; set; }
        public ICollection<SubTask> SubTasks { get; set; } = new List<SubTask>();
    }
}
