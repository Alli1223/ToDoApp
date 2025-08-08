using System.Collections.Concurrent;
using ToDoApp.Models;

public class InMemoryTodoStore : ITodoStore
{
    private readonly ConcurrentDictionary<string, ConcurrentDictionary<Guid, TodoItem>> _data = new();

    public IEnumerable<TodoItem> GetTodos(string userId)
    {
        if (_data.TryGetValue(userId, out var todos))
            return todos.Values;
        return Enumerable.Empty<TodoItem>();
    }

    public TodoItem AddTodo(string userId, string title)
    {
        var todo = new TodoItem(Guid.NewGuid(), title, false);
        var userTodos = _data.GetOrAdd(userId, _ => new ConcurrentDictionary<Guid, TodoItem>());
        userTodos[todo.Id] = todo;
        return todo;
    }

    public void UpdateTodo(string userId, Guid id, bool isComplete)
    {
        if (_data.TryGetValue(userId, out var todos) && todos.TryGetValue(id, out var existing))
        {
            todos[id] = existing with { IsComplete = isComplete };
        }
    }

    public void DeleteTodo(string userId, Guid id)
    {
        if (_data.TryGetValue(userId, out var todos))
        {
            todos.TryRemove(id, out _);
        }
    }
}
