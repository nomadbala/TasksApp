using TasksApp.Contracts;
using TasksApp.Model;

namespace TasksApp.Service;

public interface ITodoService
{
    Task<Guid> CreateAsync(NewTodoItemContract contract);
    Task UpdateAsync(Guid id, TodoItem todoItem);
    Task DeleteAsync(Guid id);
    Task MarkDoneAsync(Guid id);
    Task<List<TodoItem>> GetAllByStatusAsync(string status = "active");
}