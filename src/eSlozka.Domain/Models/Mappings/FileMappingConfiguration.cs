using AutoMapper;

namespace eSlozka.Domain.Models.Mappings;

public class FileMappingConfiguration : Profile
{
    public FileMappingConfiguration()
    {
        CreateMap<File, File>().ForMember(member => member.Folder, options => options.Ignore())
            .ForMember(member => member.Extension, options => options.Ignore())
            .ForMember(member => member.MimeType, options => options.Ignore())
            .ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}