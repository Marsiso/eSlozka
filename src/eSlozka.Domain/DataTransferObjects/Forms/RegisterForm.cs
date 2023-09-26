namespace eSlozka.Domain.DataTransferObjects.Forms;

public class RegisterForm
{
    public string? GivenName { get; set; }
    public string? FamilyName { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? PasswordRepeat { get; set; }
}