using Microsoft.EntityFrameworkCore;
using TodoApi.Models;

namespace TodoApi.Services
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public required DbSet<TodoTask> TodoTasks { get; set; }
    }
}
