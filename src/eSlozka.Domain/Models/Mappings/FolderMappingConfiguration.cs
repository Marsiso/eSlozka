using AutoMapper;

namespace eSlozka.Domain.Models.Mappings;

public class FolderMappingConfiguration : Profile
{
    public FolderMappingConfiguration()
    {
        CreateMap<Folder, Folder>().ForMember(member => member.User, options => options.Ignore())
            .ForMember(member => member.Category, options => options.Ignore())
            .ForMember(member => member.Parent, options => options.Ignore())
            .ForMember(member => member.Files, options => options.Ignore())
            .ForMember(member => member.Children, options => options.Ignore())
            .ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}
