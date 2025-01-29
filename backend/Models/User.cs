using System.Collections;

namespace backend.Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public AuthToken AuthToken { get; set; } = new();
    public ICollection<Event> MyEvents { get; set; } = new List<Event>();
    public ICollection<Role> Roles { get; set; } = new List<Role>();
    public ICollection<Event> Events { get; set; } = new List<Event>();
}
