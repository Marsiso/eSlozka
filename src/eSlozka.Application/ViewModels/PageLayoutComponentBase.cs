using System.ComponentModel;
using eSlozka.Application.Authentication;
using eSlozka.Domain.DataTransferObjects.Sessions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace eSlozka.Application.ViewModels;

public class PageLayoutComponentBase<TViewModel> : LayoutComponentBase, IDisposable where TViewModel : ViewModelBase
{
    [Inject] public required TViewModel Model { get; set; }
    [Inject] public required AuthenticationStateProvider AuthenticationStateProvider { get; set; }
    [Inject] public required ProtectedSessionStorage SessionStorage { get; set; }

    public UserSession? Session { get; private set; }

    public void Dispose()
    {
        Model.PropertyChanged -= OnModelPropertyChanged;
        AuthenticationStateProvider.AuthenticationStateChanged -= OnAuthenticationStateChanged;

        GC.SuppressFinalize(this);
    }

    public RenderFragment? GetChildren()
    {
        return Body;
    }

    public async Task Logout()
    {
        if (AuthenticationStateProvider is RevalidatingAuthenticationStateProvider revalidatingAuthenticationStateProvider) await revalidatingAuthenticationStateProvider.UpdateAuthenticationState(default);
    }

    private async void OnAuthenticationStateChanged(Task<AuthenticationState> authenticationStateTask)
    {
        var sessionStorageResult = await SessionStorage.GetAsync<UserSession>(nameof(UserSession));
        Session = sessionStorageResult.Value;
    }

    protected override bool ShouldRender()
    {
        return !Model.Busy;
    }

    protected override async Task OnInitializedAsync()
    {
        Model.PropertyChanged += OnModelPropertyChanged;
        AuthenticationStateProvider.AuthenticationStateChanged += OnAuthenticationStateChanged;

        await Model.OnViewModelInitialized();
    }

    protected override Task OnParametersSetAsync()
    {
        return Model.OnViewModelParametersSet();
    }

    protected override Task OnAfterRenderAsync(bool firstRender)
    {
        if (firstRender) return Model.OnViewModelAfterRender();

        return base.OnAfterRenderAsync(firstRender);
    }

    private async void OnModelPropertyChanged(object? sender, PropertyChangedEventArgs args)
    {
        await InvokeAsync(StateHasChanged);
    }
}
