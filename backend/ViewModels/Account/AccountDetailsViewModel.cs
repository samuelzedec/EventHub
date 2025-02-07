namespace backend.ViewModels.Account;
public class AccountDetailsViewModel
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Slug { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
}