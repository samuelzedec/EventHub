using backend.Models;

namespace backend.Extensions;
public static class TransferData
{
    public static void CopyFrom(this User receiver, User sender)
    {
        receiver.Username = sender.Username;
        receiver.Email = sender.Email;
        receiver.Password = sender.Password;
        receiver.UpdateAt = DateTime.UtcNow;
    }
}