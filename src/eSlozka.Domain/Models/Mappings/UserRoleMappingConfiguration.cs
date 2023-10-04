using AutoMapper;

namespace eSlozka.Domain.Models.Mappings;

public class UserRoleMappingConfiguration : Profile
{
    public UserRoleMappingConfiguration()
    {
        CreateMap<UserRole, UserRole>().ForMember(member => member.User, options => options.Ignore())
            .ForMember(member => member.Role, options => options.Ignore())
            .ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}