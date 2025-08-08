using ToDoApp.Models;

public interface ITodoStore
{
    IEnumerable<TodoItem> GetTodos(string userId);
    TodoItem AddTodo(string userId, string title);
    void UpdateTodo(string userId, Guid id, bool isComplete);
    void DeleteTodo(string userId, Guid id);
}
