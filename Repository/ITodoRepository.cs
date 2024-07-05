using TasksApp.Contracts;
using TasksApp.Model;

namespace TasksApp.Repository;

public interface ITodoRepository
{
    Task<Guid> CreateAsync(NewTodoItemContract contract);
    Task UpdateAsync(Guid id, TodoItem todoItem);
    Task DeleteAsync(Guid id);
    Task MarkDone(Guid id);
    Task<List<TodoItem>> GetAllActiveAsync();
    Task<List<TodoItem>> GetAllDoneAsync();
}