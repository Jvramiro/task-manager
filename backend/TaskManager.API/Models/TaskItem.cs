using TaskManager.API.Enums;

namespace TaskManager.API.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Priority Priority { get; set; } = Priority.Normal;
    public Status Status { get; set; } = Status.NotStarted;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}