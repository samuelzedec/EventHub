using System.Security.Claims;
using backend.Models;

namespace backend.Extensions;

public static class RoleClaimsExtension
{
    public static IEnumerable<Claim> GetClaims(this User user)
    {
        var result = new List<Claim>
        {
            new ("Id", user.Id.ToString()),
            new (ClaimTypes.Name, user.Username),
            new (ClaimTypes.Email, user.Email)
        };
        result.AddRange(user.Roles.Select(x => new Claim(ClaimTypes.Role, x.Name)));
        return result;
    }
}