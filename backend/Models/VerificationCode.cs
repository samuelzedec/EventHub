namespace backend.Models;
public class VerificationCode
{
    public int Id { get; set; }
    public string UserEmail { get; set; } = string.Empty;
    public int Code { get; set; }
    public DateTime Duration { get; set; }
}