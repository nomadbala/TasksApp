using System.ComponentModel.DataAnnotations;

namespace TasksApp.Model;

public class TodoItem
{
    [Required(ErrorMessage = "Id cannot be empty")]
    public Guid Id { get; set; }
    
    [MaxLength(200, ErrorMessage = "Title cannot contain more than 200 symbols")]
    public string Title { get; set; }
    
    public DateTime ActiveAt { get; set; }
    
    public bool IsDone { get; set; }

    public TodoItem(string title, DateTime activeAt, bool isDone)
    {
        Id = Guid.NewGuid();
        Title = title;
        ActiveAt = activeAt;
        IsDone = isDone;
    }
}