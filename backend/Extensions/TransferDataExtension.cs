using backend.Models;
using backend.ViewModels.Account;

namespace backend.Extensions;
public static class TransferData
{
    public static void CopyFrom(this User target, User source)
    {
        target.Username = source.Username;
        target.Email = source.Email;
        target.Password = source.Password;
        target.UpdateAt = DateTime.UtcNow;
    }

    public static void CopyFromAccessToken(this AuthToken target, AuthToken source)
    {
        target.AccessToken = source.AccessToken;
        target.AccessTokenExpiry = source.AccessTokenExpiry;
        target.UpdatedAt = DateTime.UtcNow;
    }

    public static void CopyFromRefreshToken(this AuthToken target, AuthToken source)
    {
        target.RefreshToken = source.RefreshToken;
        target.RefreshTokenExpiry = source.RefreshTokenExpiry;
        target.UpdatedAt = DateTime.UtcNow;
    }

    public static void CopyFrom(this Event target, Event source)
    {
        target.Name = source.Name;
        target.Description = source.Description;
        target.DateAndTime = source.DateAndTime;
        target.Location = source.Location;
        target.MaxCapacity = source.MaxCapacity;
        target.UpdatedAt = DateTime.Now;
    }
    public static void CopyFrom(this AccountDetailsViewModel target, User source)
    {
        target.Id = source.Id;
        target.Username = source.Username;
        target.Slug = source.Slug;
        target.Email = source.Email;
    }
}