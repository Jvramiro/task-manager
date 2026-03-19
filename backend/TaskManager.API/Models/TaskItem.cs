namespace TaskManager.API.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Priority { get; set; } = "normal";
    public string Status { get; set; } = "not started";
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}