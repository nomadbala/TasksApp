using System.ComponentModel.DataAnnotations;

namespace TasksApp.Contracts;

public class UpdateTaskContract
{
    [Required]
    [MaxLength(200)]
    public string Title { get; set; }
    
    [Required]
    public DateTime ActiveAt { get; set; }
}