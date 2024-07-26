using Microsoft.EntityFrameworkCore;
using RFTodoAPI.Models;

namespace RFTodoAPI.Data
{
    public class ApiContext: DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options): base(options)
        {
            
        }

        public DbSet<Goals> Goals { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
