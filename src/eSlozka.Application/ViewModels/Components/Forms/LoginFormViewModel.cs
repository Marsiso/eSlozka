using eSlozka.Domain.DataTransferObjects.Forms;
using MudBlazor;

namespace eSlozka.Application.ViewModels.Components.Forms;

public class LoginFormViewModel : ViewModelBase
{
    private LoginForm _form = new();
    private bool _hasMobileDeviceViewPortWidth;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
    private bool _passwordInputShowPassword;
    private InputType _passwordInputType = InputType.Password;

    public LoginForm Form
    {
        get => _form;
        set => SetValue(ref _form, value);
    }

    public bool PasswordInputShowPassword
    {
        get => _passwordInputShowPassword;
        set => SetValue(ref _passwordInputShowPassword, value);
    }

    public InputType PasswordInputType
    {
        get => _passwordInputType;
        set => SetValue(ref _passwordInputType, value);
    }

    public string PasswordInputIcon
    {
        get => _passwordInputIcon;
        set => SetValue(ref _passwordInputIcon, value);
    }

    public bool HasMobileDeviceViewPortWidth
    {
        get => _hasMobileDeviceViewPortWidth;
        set => SetValue(ref _hasMobileDeviceViewPortWidth, value);
    }

    public void OnPasswordInputShowPasswordClick()
    {
        if (PasswordInputShowPassword)
        {
            PasswordInputShowPassword = false;
            PasswordInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordInputType = InputType.Password;
        }
        else
        {
            PasswordInputShowPassword = true;
            PasswordInputIcon = Icons.Material.Filled.Visibility;
            PasswordInputType = InputType.Text;
        }
    }

    public string GetComponentWidth()
    {
        return HasMobileDeviceViewPortWidth ? "min-width: 100vw; min-height: 100vh;" : "min-width: 30rem; max-width: 30rem;";
    }
}