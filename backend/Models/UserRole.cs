namespace backend.Models;

public class UserRole
{
    public int UserId { get; set; }
    public User User { get; set; } = new();
    public int RoleId { get; set; }
    public Role Role { get; set; } = new();
    public DateTime CreatedAt { get; set; } = new();
}
