using eSlozka.Domain.Enums;

namespace eSlozka.Domain.Services;

public interface IHashProvider
{
    (string, string) GetHash(string value);
    HashVerificationResult VerifyHash(string value, string base64Key, string base64Salt);
}