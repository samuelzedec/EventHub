namespace backend.Models;

public class UserEvent
{
    public User User { get; set; } = new();
    public Event Event { get; set; } = new();
    public DateTime CreatedAt { get; set; }
}