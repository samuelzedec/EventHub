using SecureIdentity.Password;

namespace backend.Services;

public class PasswordService
{
    public string GenerateHash()
    {
        var password = PasswordGenerator.Generate(25, true, false);
        return PasswordHasher.Hash(password);
    }
}