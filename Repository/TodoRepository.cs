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

        var todo = new TodoItem
        {
            Title = contract.Title,
            ActiveAt = contract.ActiveAt
        };
    
        await _context.AddAsync(todo);
        await _context.SaveChangesAsync();

        return todo.Id;
    }

    public async Task UpdateAsync(Guid id, UpdateTaskContract contract)
    {
        var existingTask = await _context.Todos.FirstOrDefaultAsync(t => t.Id == id);
        if (existingTask == null)
        {
            throw new ElementNotFoundException($"Task with ID {id} not found");
        }

        if (await _context.Todos.AnyAsync(t => t.Id != id && t.Title == contract.Title && t.ActiveAt.Date == contract.ActiveAt.Date))
        {
            throw new ElementNotFoundException($"Task with title '{contract.Title}' and date '{contract.ActiveAt}' already exists");
        }

        existingTask.Title = contract.Title;
        existingTask.ActiveAt = contract.ActiveAt.Date;

        await _context.SaveChangesAsync();
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
            .Where(t => t.ActiveAt <= DateTime.UtcNow)
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