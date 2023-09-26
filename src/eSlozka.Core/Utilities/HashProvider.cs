using System.Security.Cryptography;
using System.Text;
using eSlozka.Domain.Enums;
using eSlozka.Domain.Services;
using Microsoft.Extensions.Options;

namespace eSlozka.Core.Utilities;

public class HashProvider : IHashProvider
{
    public readonly HashProviderOptions Options;

    public HashProvider(IOptions<HashProviderOptions> options)
    {
        Options = options.Value;
    }

    public (string, string) GetHash(string value)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);

        var pepperedValue = value + Options.Pepper;

        Span<byte> pepperedValueBytes = stackalloc byte[pepperedValue.Length];
        Encoding.UTF8.GetBytes(pepperedValue, pepperedValueBytes);

        Span<byte> saltBytes = stackalloc byte[Options.SaltSize];
        RandomNumberGenerator.Fill(saltBytes);

        Span<byte> keyBytes = stackalloc byte[Options.KeySize];
        Rfc2898DeriveBytes.Pbkdf2(pepperedValueBytes, saltBytes, keyBytes, Options.Cycles, HashAlgorithmName.SHA512);

        return (Convert.ToBase64String(keyBytes), Convert.ToBase64String(saltBytes));
    }

    public HashVerificationResult VerifyHash(string value, string base64Key, string base64Salt)
    {
        ArgumentException.ThrowIfNullOrEmpty(value);
        ArgumentException.ThrowIfNullOrEmpty(base64Key);
        ArgumentException.ThrowIfNullOrEmpty(base64Salt);

        var pepperedValue = value + Options.Pepper;

        Span<byte> pepperedValueBytes = stackalloc byte[pepperedValue.Length];
        Encoding.UTF8.GetBytes(pepperedValue, pepperedValueBytes);

        var keyBytes = Convert.FromBase64String(base64Key);
        var saltBytes = Convert.FromBase64String(base64Salt);

        Span<byte> newKeyBytes = stackalloc byte[Options.KeySize];
        Rfc2898DeriveBytes.Pbkdf2(pepperedValueBytes, saltBytes, newKeyBytes, Options.Cycles, HashAlgorithmName.SHA512);

        return CryptographicOperations.FixedTimeEquals(keyBytes, newKeyBytes)
            ? HashVerificationResult.Succeeded
            : HashVerificationResult.Failed;
    }
}