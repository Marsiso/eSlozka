using eSlozka.Domain.Models.Common;

namespace eSlozka.Domain.Models;

public class File : ChangeTrackingEntity
{
    public int FileID { get; set; }
    public int FolderID { get; set; }
    public int MimeTypeID { get; set; }
    public int ExtensionID { get; set; }
    public string SafeName { get; set; } = string.Empty;
    public string UnsafeName { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public long Size { get; set; }

    public Folder? Folder { get; set; }
    public CodeListItem? MimeType { get; set; }
    public CodeListItem? Extension { get; set; }
}
