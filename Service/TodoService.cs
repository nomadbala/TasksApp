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

    public async Task<Guid> CreateAsync(CreateTaskContract contract)
    {
        if (contract.ActiveAt < DateTime.MinValue || contract.ActiveAt > DateTime.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(contract.ActiveAt));
        
        contract.ActiveAt = DateTime.SpecifyKind(contract.ActiveAt, DateTimeKind.Utc);
        
        return await _repository.CreateAsync(contract);
    }

    public async Task UpdateAsync(Guid id, UpdateTaskContract contract)
    {
        if (contract.ActiveAt < DateTime.MinValue || contract.ActiveAt > DateTime.MaxValue)
            throw new ArgumentOutOfRangeException(nameof(contract.ActiveAt));

        contract.ActiveAt = DateTime.SpecifyKind(contract.ActiveAt, DateTimeKind.Utc);
        
        await _repository.UpdateAsync(id, contract);
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
        if (status == "done")
        {
            return await _repository.GetAllDoneAsync();
        }
        else
        {
            return await _repository.GetAllActiveAsync();
        }
    }
}