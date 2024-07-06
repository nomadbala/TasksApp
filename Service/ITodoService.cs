using TasksApp.Contracts;
using TasksApp.Model;

namespace TasksApp.Service;

public interface ITodoService
{
    Task<Guid> CreateAsync(CreateTaskContract contract);
    Task UpdateAsync(Guid id, UpdateTaskContract contract);
    Task DeleteAsync(Guid id);
    Task MarkDoneAsync(Guid id);
    Task<List<TodoItem>> GetAllByStatusAsync(string status = "active");
}