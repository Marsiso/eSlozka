using eSlozka.Domain.Enums;

namespace eSlozka.Domain.Helpers;

public static class PermissionHelpers
{
    public static List<Permission> GetAll()
    {
        return Enum.GetValues(typeof(Permission)).OfType<Permission>().ToList();
    }
}