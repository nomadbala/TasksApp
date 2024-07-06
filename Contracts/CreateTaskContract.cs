using System.ComponentModel.DataAnnotations;

namespace TasksApp.Contracts;

public class CreateTaskContract
{
    [Required]
    [MaxLength(200)]
    public String Title { get; set; }
    
    [Required]
    public DateTime ActiveAt { get; set; }
}