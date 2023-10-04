using AutoMapper;

namespace eSlozka.Domain.Models.Mappings;

public class AuditMappingConfiguration : Profile
{
    public AuditMappingConfiguration()
    {
        CreateMap<Audit, Audit>().ForMember(member => member.User, options => options.Ignore());
    }
}