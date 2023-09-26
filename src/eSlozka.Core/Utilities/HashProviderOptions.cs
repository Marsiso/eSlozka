namespace eSlozka.Core.Utilities;

public class HashProviderOptions
{
    public const string SectionName = "Hash";

    public required int Cycles = 2_000_000;
    public required int KeySize = 32;
    public required int SaltSize = 16;
    public required string? Pepper { get; set; }
}