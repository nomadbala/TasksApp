using System.ComponentModel.DataAnnotations;

namespace TasksApp.Model;

public class TodoItem
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(200, ErrorMessage = "Title cannot contain more than 200 symbols")]
    public string Title { get; set; }
    
    public DateTime ActiveAt { get; set; }
    
    public bool IsDone { get; set; }

    public TodoItem() { }

    public TodoItem(string title, DateTime activeAt)
    {
        Id = Guid.NewGuid();
        Title = title;
        ActiveAt = DateTime.SpecifyKind(activeAt, DateTimeKind.Utc);
        IsDone = false;
    }
}