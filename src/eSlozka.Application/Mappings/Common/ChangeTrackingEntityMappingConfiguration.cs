using AutoMapper;
using eSlozka.Domain.Models.Common;

namespace eSlozka.Application.Mappings.Common;

public class ChangeTrackingEntityMappingConfiguration : Profile
{
    public ChangeTrackingEntityMappingConfiguration()
    {
        CreateMap<ChangeTrackingEntity, ChangeTrackingEntity>().ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}