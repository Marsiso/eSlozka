using Microsoft.AspNetCore.Components;

namespace eSlozka.Application.ViewModels.Shared.Layouts;

public class MainLayoutViewModel : ViewModelBase
{
    private bool _sidebarVisible;

    public readonly NavigationManager Navigation;

    public MainLayoutViewModel(NavigationManager navigation)
    {
        Navigation = navigation;
    }

    public bool SidebarVisible
    {
        get => _sidebarVisible;
        set => SetValue(ref _sidebarVisible, value);
    }

    public void ToggleSidebar()
    {
        SidebarVisible = !SidebarVisible;
    }
}
