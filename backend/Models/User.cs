namespace backend.Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsEmailVerified { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public AuthToken AuthToken { get; set; } = null!;
    public ICollection<Event> MyEvents { get; set; } = new List<Event>();
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
