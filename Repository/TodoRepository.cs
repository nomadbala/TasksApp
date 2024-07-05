using Microsoft.EntityFrameworkCore;
using TasksApp.Contracts;
using TasksApp.Model;

namespace TasksApp.Repository;

public class TodoRepository : ITodoRepository
{
    private readonly DatabaseContext _context;

    public TodoRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(NewTodoItemContract contract)
    {
        var todo = new TodoItem(contract.Title, DateTime.Today, false);
        
        await _context.AddAsync(todo);
        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task UpdateAsync(Guid id, TodoItem todoItem)
    {
        await _context.Todos
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Title, t => todoItem.Title)
                .SetProperty(t => t.ActiveAt, t => todoItem.ActiveAt)
                .SetProperty(t => t.IsDone, t => todoItem.IsDone));
    }

    public async Task DeleteAsync(Guid id)
    {
        await _context.Todos
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task MarkDone(Guid id)
    {
        await _context.Todos
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.IsDone, t => true));
    }

    public async Task<List<TodoItem>> GetAllActiveAsync()
    {
        return await _context.Todos
            .Where(t => !t.IsDone)
            .Where(t => t.ActiveAt.Date <= DateTime.Now)
            .ToListAsync();
    }

    public async Task<List<TodoItem>> GetAllDoneAsync()
    {
        return await _context.Todos
            .Where(t => t.IsDone)
            .ToListAsync();
    }
}