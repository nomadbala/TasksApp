using Microsoft.AspNetCore.Mvc;
using TasksApp.Contracts;
using TasksApp.Model;
using TasksApp.Service;

namespace TasksApp.Controller;

[ApiController]
[Route("/api/todo-list/tasks")]
public class TodoController : ControllerBase
{
    private readonly ITodoService _service;

    public TodoController(ITodoService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateAsync([FromBody] NewTodoItemContract contract)
    {
        if (contract == null)
            return BadRequest();

        try
        {
            var id = await _service.CreateAsync(contract);

            if (id == Guid.Empty)
                return StatusCode(301);

            return CreatedAtAction(nameof(CreateAsync), new { id }, id);
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAsync(Guid id, [FromBody] TodoItem todoItem)
    {
        if (id == Guid.Empty || todoItem == null)
            return BadRequest();

        try
        {
            await _service.UpdateAsync(id, todoItem);

            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        try
        {
            await _service.DeleteAsync(id);

            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpPost("{id}/done")]
    public async Task<ActionResult> MarkDoneAsync(Guid id)
    {
        if (id == Guid.Empty)
            return BadRequest();

        try
        {
            await _service.MarkDoneAsync(id);

            return NoContent();
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<TodoItem>>> GetAllByStatus([FromQuery] string status)
    {
        if (string.IsNullOrEmpty(status))
            return BadRequest();

        try
        {
            return Ok(await _service.GetAllByStatusAsync(status));
        }
        catch (KeyNotFoundException)
        {
            return NotFound();
        }
        catch (Exception e)
        {
            return StatusCode(500, e.Message);
        }
    }
}