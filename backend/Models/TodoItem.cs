namespace ToDoApp.Models;

public record TodoItem(Guid Id, string Title, bool IsComplete);
