namespace backend.Models;

public class Event
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public DateTime DateAndTime { get; set; }
    public string Location { get; set; } = string.Empty;
    public int MaxCapacity { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public User Creator { get; set; } = new();
    public ICollection<User> Users { get; set; } = new List<User>(); 
}