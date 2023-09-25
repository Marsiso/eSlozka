using AutoMapper;

namespace eSlozka.Domain.Models.Mappings;

public class PermissionMappingConfiguration : Profile
{
    public PermissionMappingConfiguration()
    {
        CreateMap<UserRole, UserRole>().ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}