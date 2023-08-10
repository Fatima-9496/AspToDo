using FinalTodo.Models;
using Microsoft.EntityFrameworkCore;

namespace FinalTodo.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<UserTask> UserTasks { get; set; }
    }
}




