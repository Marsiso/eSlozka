namespace eSlozka.Domain.Enums;

[Flags]
public enum Permission
{
    None = 0,
    ViewRoles = 1,
    EditRoles = 2,
    ViewUsers = 4,
    EditUsers = 8,
    ViewFiles = 16,
    EditFiles = 32,
    ViewCodeLists = 64,
    EditCodeLists = 128,
    ShareFiles = 256,
    All = ViewRoles | EditRoles | ViewUsers | EditUsers | ViewFiles | EditFiles | ViewCodeLists | EditCodeLists | ShareFiles
}
