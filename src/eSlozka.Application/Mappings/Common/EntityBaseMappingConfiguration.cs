using AutoMapper;
using eSlozka.Domain.Models.Common;

namespace eSlozka.Application.Mappings.Common;

public class EntityBaseMappingConfiguration : Profile
{
    public EntityBaseMappingConfiguration()
    {
        CreateMap<EntityBase, EntityBase>();
    }
}