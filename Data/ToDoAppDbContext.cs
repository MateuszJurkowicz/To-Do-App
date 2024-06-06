using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using To_Do_App.Data.Entities;

namespace To_Do_App.Data
{
    public class ToDoAppDbContext : DbContext
    {
        public DbSet<WorkTask> WorkTasks { get; set; }
        public DbSet<SubTask> SubTasks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=ToDoAppDb;Trusted_Connection=True;");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<SubTask>()
                .HasOne(s => s.WorkTask)
                .WithMany(w => w.SubTasks)
                .HasForeignKey(s => s.WorkTaskId);
        }
    }
}
