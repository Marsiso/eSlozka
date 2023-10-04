using eSlozka.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using File = eSlozka.Domain.Models.File;

namespace eSlozka.Data;

public class DataContext : DbContext
{
    private readonly ISaveChangesInterceptor _saveChangesInterceptor;

    public DataContext(DbContextOptions options, ISaveChangesInterceptor saveChangesInterceptor) : base(options)
    {
        _saveChangesInterceptor = saveChangesInterceptor;
    }

    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Role> Roles { get; set; } = default!;
    public DbSet<Permission> Permissions { get; set; } = default!;
    public DbSet<UserRole> UserRoles { get; set; } = default!;
    public DbSet<Folder> Folders { get; set; } = default!;
    public DbSet<File> Files { get; set; } = default!;
    public DbSet<CodeList> CodeLists { get; set; } = default!;
    public DbSet<CodeListItem> CodeListItems { get; set; } = default!;
    public DbSet<Audit> Audits { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);

        optionsBuilder.AddInterceptors(_saveChangesInterceptor);
    }
}