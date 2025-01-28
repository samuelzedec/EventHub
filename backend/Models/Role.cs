namespace backend.Models;

public class Role
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>(); 
}
