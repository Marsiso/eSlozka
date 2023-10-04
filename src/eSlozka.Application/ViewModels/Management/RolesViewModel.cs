using eSlozka.Core.Queries.Roles;
using eSlozka.Domain.Constants;
using eSlozka.Domain.Models;
using MediatR;
using MudBlazor;

namespace eSlozka.Application.ViewModels.Management;

public class RolesViewModel : ViewModelBase
{
    private readonly ISender _sender;

    public List<Role> Roles
    {
        get => _roles;
        set => SetValue(ref _roles, value);
    }

    public readonly List<BreadcrumbItem> BreadCrumbData = new()
    {
        new BreadcrumbItem(nameof(Routes.Home), href: Routes.Home, icon: Icons.Material.Rounded.Home),
        new BreadcrumbItem(nameof(Routes.Management), href: Routes.Home, disabled: true, icon: Icons.Material.Rounded.Dashboard),
        new BreadcrumbItem(nameof(Routes.Management.Roles), href: Routes.Management.Roles, disabled: true, icon: Icons.Material.Rounded.Security),
    };

    private List<Role> _roles = Enumerable.Empty<Role>().ToList();

    public RolesViewModel(ISender sender)
    {
        _sender = sender;

       ViewModelInitialized += InitialLoad;
    }

    private async Task InitialLoad()
    {
        Busy = true;

        var query = new GetRolesQuery();
        Roles = await _sender.Send(query);

        Busy = false;
    }

    public void Deconstruct()
    {
        ViewModelInitialized -= InitialLoad;
    }
}
