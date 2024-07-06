using Microsoft.EntityFrameworkCore;
using TasksApp.Model;

namespace TasksApp;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

    public DbSet<TodoItem> Todos { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();

            entity.Property(e => e.Title).IsRequired().HasMaxLength(200);

            // Если вы действительно хотите уникальный индекс, оставьте эту строку
            // В противном случае, удалите или закомментируйте её
            // entity.HasIndex(e => new { e.Title, e.ActiveAt }).IsUnique();
        });

        base.OnModelCreating(modelBuilder);
    }
}