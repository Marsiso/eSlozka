namespace eSlozka.Domain.Models;

public class Audit
{
    public int AuditID { get; set; }
    public int EntityID { get; set; }
    public int UserID { get; set; }
    public DateTime DateCreated { get; set; }
    public string Type { get; set; } = string.Empty;
    public string EntityType { get; set; } = string.Empty;
    public string? OldValue { get; set; }
    public string? NewValue { get; set; }
    public string? AffectedColumns { get; set; } = string.Empty;

    public User? User { get; set; }
}
