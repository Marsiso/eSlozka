using AutoMapper;
using eSlozka.Domain.Models;

namespace eSlozka.Application.Mappings;

public class AuditMappingConfiguration : Profile
{
    public AuditMappingConfiguration()
    {
        CreateMap<Audit, Audit>().ForMember(member => member.User, options => options.Ignore());
    }
}