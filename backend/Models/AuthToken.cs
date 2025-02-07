namespace backend.Models;

public class AuthToken
{
    public int Id { get; set; }
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public DateTime AccessTokenExpiry { get; set; }
    public DateTime RefreshTokenExpiry { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt {get;set;}
    public User User { get; set; } = null!;
}