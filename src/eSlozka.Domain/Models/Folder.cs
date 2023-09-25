using eSlozka.Domain.Models.Common;

namespace eSlozka.Domain.Models;

public class Folder : ChangeTrackingEntity
{
    public int FolderID { get; set; }
    public int UserID { get; set; }
    public int? ParentID { get; set; }
    public int? CategoryID { get; set; }
    public string Name { get; set; } = string.Empty;
    public long TotalSize { get; set; }
    public int TotalCount { get; set; }

    public User? User { get; set; }
    public Folder? Parent { get; set; }
    public CodeListItem? Category { get; set; }
    public ICollection<File>? Files { get; set; }
    public ICollection<Folder>? Children { get; set; }
}