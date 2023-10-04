namespace eSlozka.Domain.DataTransferObjects.Users;

public class RegisterInput
{
    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordRepeat { get; set; }
}
