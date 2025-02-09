using Microsoft.EntityFrameworkCore;
using MVCApplicationToDo.Models;
using System.Collections.Generic;

namespace MVCApplicationToDo.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<TaskItem> TaskItems { get; set; }
    }
}
