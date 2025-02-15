using SecureIdentity.Password;

namespace backend.Services;

public class PasswordService
{
    public (string, string) GenerateHash()
    {
        var password = PasswordGenerator.Generate(25, true, false);
        return (password, PasswordHasher.Hash(password));
    }

    public bool Decrypting(string hash, string password)
        => PasswordHasher.Verify(hash, password);
}