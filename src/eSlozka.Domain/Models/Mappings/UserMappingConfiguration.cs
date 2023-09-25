using AutoMapper;

namespace eSlozka.Domain.Models.Mappings;

public class UserMappingConfiguration : Profile
{
    public UserMappingConfiguration()
    {
        CreateMap<User, User>().ForMember(member => member.Roles, options => options.Ignore())
            .ForMember(member => member.Folders, options => options.Ignore())
            .ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}
