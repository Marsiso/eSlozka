using AutoMapper;

namespace eSlozka.Domain.Models.Mappings;

public class RoleMappingConfiguration : Profile
{
    public RoleMappingConfiguration()
    {
        CreateMap<Role, Role>().ForMember(member => member.Users, options => options.Ignore())
            .ForMember(member => member.Permissions, options => options.Ignore())
            .ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}
