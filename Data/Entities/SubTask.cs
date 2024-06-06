using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace To_Do_App.Data.Entities
{
    public class SubTask
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }
        public int WorkTaskId { get; set; }
        public WorkTask WorkTask { get; set; }
    }
}
