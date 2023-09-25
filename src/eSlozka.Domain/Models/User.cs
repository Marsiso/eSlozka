using eSlozka.Domain.Models.Common;

namespace eSlozka.Domain.Models;

public class User : ChangeTrackingEntity
{
    public int UserID { get; set; }
    public string GivenName { get; set; } = string.Empty;
    public string FamilyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public bool EmailConfirmed { get; set; }
    public string? Password { get; set; }
    public string? PasswordSalt { get; set; }
    public string? Locale { get; set; }
    public string? ProfilePhoto { get; set; }
    public bool DarkThemeEnabled { get; set; }
    public bool Verified { get; set; }

    public ICollection<UserRole>? Roles { get; set; }
    public ICollection<Folder>? Folders { get; set; }
}