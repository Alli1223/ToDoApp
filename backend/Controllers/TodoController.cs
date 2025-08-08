using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDoApp.Models;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class TodoController : ControllerBase
{
    private readonly ITodoStore _store;

    public TodoController(ITodoStore store)
    {
        _store = store;
    }

    [HttpGet]
    public IEnumerable<TodoItem> Get()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        return _store.GetTodos(userId);
    }

    [HttpPost]
    public ActionResult<TodoItem> Post([FromBody] string title)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var item = _store.AddTodo(userId, title);
        return Created($"/api/todo/{item.Id}", item);
    }

    [HttpPut("{id}")]
    public IActionResult Put(Guid id, [FromBody] bool isComplete)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        _store.UpdateTodo(userId, id, isComplete);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        _store.DeleteTodo(userId, id);
        return NoContent();
    }
}
