using AutoMapper;
using eSlozka.Domain.Models;

namespace eSlozka.Application.Mappings;

public class RoleMappingConfiguration : Profile
{
    public RoleMappingConfiguration()
    {
        CreateMap<Role, Role>().ForMember(member => member.Users, options => options.Ignore())
            .ForMember(member => member.Permission, options => options.Ignore())
            .ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}