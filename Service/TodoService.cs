using TasksApp.Contracts;
using TasksApp.Model;
using TasksApp.Repository;

namespace TasksApp.Service;

public class TodoService : ITodoService
{
    private readonly ITodoRepository _repository;

    public TodoService(ITodoRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> CreateAsync(NewTodoItemContract contract)
    {
        return await _repository.CreateAsync(contract);
    }

    public async Task UpdateAsync(Guid id, TodoItem todoItem)
    {
        await _repository.UpdateAsync(id, todoItem);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task MarkDoneAsync(Guid id)
    {
        await _repository.MarkDone(id);
    }

    public async Task<List<TodoItem>> GetAllByStatusAsync(string status = "active")
    {
        if (status == "active")
        {
            return await _repository.GetAllActiveAsync();
        }
        else if (status == "done")
        {
            return await _repository.GetAllDoneAsync();
        }

        throw new Exception();
    }
}