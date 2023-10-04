using AutoMapper;
using eSlozka.Core.Commands.Users;
using eSlozka.Domain.Constants;
using eSlozka.Domain.DataTransferObjects.Users;
using eSlozka.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Components;
using MudBlazor;

namespace eSlozka.Application.ViewModels.Authentication;

public class RegisterViewModel : ViewModelBase
{
    private readonly IMapper _mapper;
    private readonly NavigationManager _navigation;
    private readonly ISender _sender;

    private RegisterInput _form = new();

    private bool _hasMobileDeviceViewPortWidth;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
    private bool _passwordInputShowPassword;
    private InputType _passwordInputType = InputType.Password;
    private string _passwordRepeatInputIcon = Icons.Material.Filled.VisibilityOff;
    private bool _passwordRepeatInputShowPassword;
    private InputType _passwordRepeatInputType = InputType.Password;
    private EntityValidationException? _validationException;

    public RegisterViewModel(ISender sender, NavigationManager navigation, IMapper mapper)
    {
        _sender = sender;
        _navigation = navigation;
        _mapper = mapper;
    }

    public RegisterInput Form
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

    public EntityValidationException? ValidationException
    {
        get => _validationException;
        set => SetValue(ref _validationException, value);
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

    public async Task OnRegisterButtonClick()
    {
        Busy = true;

        var command = _mapper.Map<RegisterCommand>(Form);
        var result = await _sender.Send(command);

        if (result.Result == RegisterResultType.Succeeded) _navigation.NavigateTo(Routes.Home, true);

        ValidationException = result.ValidationException;

        Busy = false;
    }

    public void OnLoginButtonClick()
    {
        _navigation.NavigateTo(Routes.Authentication.Login, true);
    }

    public bool HasError(string propertyName)
    {
        ArgumentException.ThrowIfNullOrEmpty(propertyName);

        return ValidationException?.ValidationErrors.ContainsKey(propertyName) ?? false;
    }

    public string GetErrorText(string propertyName)
    {
        ArgumentException.ThrowIfNullOrEmpty(propertyName);

        if (HasError(propertyName)) return ValidationException?.ValidationErrors[propertyName].FirstOrDefault() ?? string.Empty;
        return string.Empty;
    }
}