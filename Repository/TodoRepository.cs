using Microsoft.EntityFrameworkCore;
using TasksApp.Contracts;
using TasksApp.Exceptions;
using TasksApp.Model;
using TasksApp.Service;

namespace TasksApp.Repository;

public class TodoRepository : ITodoRepository
{
    private readonly DatabaseContext _context;

    public TodoRepository(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAsync(CreateTaskContract contract)
    {
        if (await _context.Todos.AnyAsync(t => t.Title == contract.Title && t.ActiveAt == contract.ActiveAt))
            throw new ElementAlreadyExistsException($"Task with title ${contract.Title} and date ${contract.ActiveAt} already exists");

        contract.ActiveAt = DateTime.SpecifyKind(contract.ActiveAt, DateTimeKind.Utc);
        
        var todo = new TodoItem
        {
            Title = contract.Title,
            ActiveAt = DateTime.SpecifyKind(contract.ActiveAt, DateTimeKind.Utc)
        };
    
        await _context.AddAsync(todo);
        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateTaskContract contract)
    {
        if (!await _context.Todos.AnyAsync(t => t.Title == contract.Title && t.ActiveAt == contract.ActiveAt))
            throw new ElementNotFoundException($"Task with title ${contract.Title} and date ${contract.ActiveAt} not found");
        
        await _context.Todos
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.Title, t => contract.Title)
                .SetProperty(t => t.ActiveAt, t => DateTime.SpecifyKind(contract.ActiveAt, DateTimeKind.Utc)));
    }

    public async Task DeleteAsync(Guid id)
    {
        if (!await _context.Todos.AnyAsync(t => t.Id == id))
            throw new ElementNotFoundException($"Task with id ${id} not found");
        
        await _context.Todos
            .Where(t => t.Id == id)
            .ExecuteDeleteAsync();
    }

    public async Task MarkDone(Guid id)
    {
        if (!await _context.Todos.AnyAsync(t => t.Id == id))
            throw new ElementNotFoundException($"Task with id ${id} not found");
        
        await _context.Todos
            .Where(t => t.Id == id)
            .ExecuteUpdateAsync(s => s
                .SetProperty(t => t.IsDone, t => true));
    }

    public async Task<List<TodoItem>> GetAllActiveAsync()
    {
        var today = DateTime.UtcNow.Date;

        var isWeekend = today.DayOfWeek == DayOfWeek.Saturday || today.DayOfWeek == DayOfWeek.Sunday;
        
        var tasks = await _context.Todos
            .Where(t => !t.IsDone)
            .Where(t => t.ActiveAt.Date <= DateTime.UtcNow)
            .ToListAsync();
        
        if (isWeekend)
        {
            tasks.ForEach(t => t.Title = $"ВЫХОДНОЙ - {t.Title}");
        }

        return tasks;
    }

    public async Task<List<TodoItem>> GetAllDoneAsync()
    {
        return await _context.Todos
            .Where(t => t.IsDone)
            .ToListAsync();
    }
}