namespace eSlozka.Application.ViewModels.Shared.Layouts;

public class MainLayoutViewModel : ViewModelBase
{
    private bool _sidebarVisible;

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