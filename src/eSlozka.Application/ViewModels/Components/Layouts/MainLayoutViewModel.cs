namespace eSlozka.Application.ViewModels.Components.Layouts;

public class MainLayoutViewModel : ViewModelBase
{
    private bool isSidebarVisible;

    public bool IsSidebarVisible
    {
        get => isSidebarVisible;
        set => SetValue(ref isSidebarVisible, value);
    }

    public void ToggleSidebar()
    {
        IsSidebarVisible = !IsSidebarVisible;
    }
}