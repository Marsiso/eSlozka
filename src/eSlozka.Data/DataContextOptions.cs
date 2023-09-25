namespace eSlozka.Data;

public class DataContextOptions
{
    public const string SectionName = "Database";

    public required string Location { get; set; } = ".\\";
    public required string FileNameWithExtension { get; set; } = "eSlozka.db";
}