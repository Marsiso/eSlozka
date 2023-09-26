using AutoMapper;
using eSlozka.Domain.Models;

namespace eSlozka.Application.Mappings;

public class PermissionMappingConfiguration : Profile
{
    public PermissionMappingConfiguration()
    {
        CreateMap<UserRole, UserRole>().ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}