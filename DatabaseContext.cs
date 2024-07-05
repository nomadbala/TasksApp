using Microsoft.EntityFrameworkCore;
using TasksApp.Model;

namespace TasksApp;

public class DatabaseContext(DbContextOptions<DatabaseContext> options) : DbContext(options)
{
    public DbSet<TodoItem> Todos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>()
            .HasIndex(ti => new { ti.Title, ti.ActiveAt })
            .IsUnique();

        base.OnModelCreating(modelBuilder);
    }
}