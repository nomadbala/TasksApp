using TasksApp.Contracts;
using TasksApp.Model;

namespace TasksApp.Repository;

public interface ITodoRepository
{
    Task<Guid> CreateAsync(CreateTaskContract contract);
    Task UpdateAsync(Guid id, UpdateTaskContract contract);
    Task DeleteAsync(Guid id);
    Task MarkDone(Guid id);
    Task<List<TodoItem>> GetAllActiveAsync();
    Task<List<TodoItem>> GetAllDoneAsync();
}