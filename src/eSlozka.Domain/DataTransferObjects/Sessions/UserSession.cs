namespace eSlozka.Domain.DataTransferObjects.Sessions;

public class UserSession
{
    public string ProtectedUserID { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public string GivenName { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string SecurityStamp { get; set; } = string.Empty;
    public string? ProfilePhoto { get; set; }
    public DateTime DateExpires { get; set; }
    public bool DarkThemeEnabled { get; set; }
}