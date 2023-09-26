using eSlozka.Domain.DataTransferObjects.Forms;
using MudBlazor;

namespace eSlozka.Application.ViewModels.Components.Forms;

public class RegisterFormViewModel : ViewModelBase
{
    private RegisterForm _form = new();
    private bool _hasMobileDeviceViewPortWidth;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
    private bool _passwordInputShowPassword;
    private InputType _passwordInputType = InputType.Password;
    private string _passwordRepeatInputIcon = Icons.Material.Filled.VisibilityOff;
    private bool _passwordRepeatInputShowPassword;
    private InputType _passwordRepeatInputType = InputType.Password;

    public RegisterForm Form
    {
        get => _form;
        set => SetValue(ref _form, value);
    }

    public bool PasswordInputShowPassword
    {
        get => _passwordInputShowPassword;
        set => SetValue(ref _passwordInputShowPassword, value);
    }

    public bool PasswordRepeatInputShowPassword
    {
        get => _passwordRepeatInputShowPassword;
        set => SetValue(ref _passwordRepeatInputShowPassword, value);
    }

    public InputType PasswordInputType
    {
        get => _passwordInputType;
        set => SetValue(ref _passwordInputType, value);
    }

    public InputType PasswordRepeatInputType
    {
        get => _passwordRepeatInputType;
        set => SetValue(ref _passwordRepeatInputType, value);
    }

    public string PasswordInputIcon
    {
        get => _passwordInputIcon;
        set => SetValue(ref _passwordInputIcon, value);
    }

    public string PasswordRepeatInputIcon
    {
        get => _passwordRepeatInputIcon;
        set => SetValue(ref _passwordRepeatInputIcon, value);
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

    public void OnPasswordRepeatInputShowPasswordClick()
    {
        if (PasswordRepeatInputShowPassword)
        {
            PasswordRepeatInputShowPassword = false;
            PasswordRepeatInputIcon = Icons.Material.Filled.VisibilityOff;
            PasswordRepeatInputType = InputType.Password;
        }
        else
        {
            PasswordRepeatInputShowPassword = true;
            PasswordRepeatInputIcon = Icons.Material.Filled.Visibility;
            PasswordRepeatInputType = InputType.Text;
        }
    }

    public string GetComponentWidth()
    {
        return HasMobileDeviceViewPortWidth ? "min-width: 100vw; min-height: 100vh;" : "min-width: 30rem; max-width: 30rem;";
    }
}