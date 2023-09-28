using AutoMapper;
using eSlozka.Application.Authentication;
using eSlozka.Core.Commands.Users;
using eSlozka.Domain.Constants;
using eSlozka.Domain.DataTransferObjects.Forms;
using eSlozka.Domain.DataTransferObjects.Sessions;
using eSlozka.Domain.Enums;
using eSlozka.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using MudBlazor;

namespace eSlozka.Application.ViewModels.Authentication;

public class LoginViewModel : ViewModelBase
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;
    private readonly IMapper _mapper;
    private readonly NavigationManager _navigation;
    private readonly ISender _sender;

    private LoginForm _form = new();
    private bool _hasMobileDeviceViewPortWidth;
    private string _passwordInputIcon = Icons.Material.Filled.VisibilityOff;
    private bool _passwordInputShowPassword;
    private InputType _passwordInputType = InputType.Password;
    private EntityValidationException? _validationException;

    public LoginViewModel(IMapper mapper, NavigationManager navigation, ISender sender, AuthenticationStateProvider authenticationStateProvider)
    {
        _mapper = mapper;
        _navigation = navigation;
        _sender = sender;
        _authenticationStateProvider = authenticationStateProvider;
    }

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

    public string GetComponentWidth()
    {
        return HasMobileDeviceViewPortWidth ? "min-width: 100vw; min-height: 100vh;" : "min-width: 30rem; max-width: 30rem;";
    }

    public async Task OnLoginButtonClick()
    {
        var command = _mapper.Map<VerifyCredentialsCommand>(Form);
        var result = await _sender.Send(command);

        if (_authenticationStateProvider is RevalidatingAuthenticationStateProvider revalidatingAuthenticationStateProvider && result.Result == VerifyCredentialsResultType.Succeeded)
        {
            var userSession = _mapper.Map<UserSession>(result.User);

            userSession.Permissions = result.User?.Roles
                ?.Select(role => role.Role?.Permission)
                .Aggregate((left, right) => left | right) ?? Permission.None;

            await revalidatingAuthenticationStateProvider.UpdateAuthenticationState(userSession);

            _navigation.NavigateTo(Routes.Home);
        }

        ValidationException = result.ValidationException;
    }

    public void OnRegisterButtonClick()
    {
        _navigation.NavigateTo(Routes.Authentication.Register, true);
    }

    public bool HasError(string propertyName)
    {
        ArgumentException.ThrowIfNullOrEmpty(propertyName);

        return ValidationException?.ValidationErrors.ContainsKey(propertyName) ?? false;
    }

    public string GetResource(string propertyName)
    {
        ArgumentException.ThrowIfNullOrEmpty(propertyName);

        if (HasError(propertyName)) return ValidationException?.ValidationErrors[propertyName].FirstOrDefault() ?? string.Empty;
        return string.Empty;
    }
}