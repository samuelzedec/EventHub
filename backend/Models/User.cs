namespace backend.Models;
public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdateAt { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); 
}
