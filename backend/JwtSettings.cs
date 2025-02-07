namespace backend;

public class JwtSettings
{
    public required string SecretKey { get; set; } 
    public required string RefreshSecretKey { get; set; }
}