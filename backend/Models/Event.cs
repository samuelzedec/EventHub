namespace backend.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public DateTime Time { get; set; }
    public string Location { get; set; } = string.Empty;
    public int MaxCapacity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public User CreateBy { get; set; } = new();
}
