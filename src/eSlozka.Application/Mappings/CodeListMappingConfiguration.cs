using AutoMapper;
using eSlozka.Domain.Models;

namespace eSlozka.Application.Mappings;

public class CodeListMappingConfiguration : Profile
{
    public CodeListMappingConfiguration()
    {
        CreateMap<CodeList, CodeList>().ForMember(member => member.CodeListItems, options => options.Ignore())
            .ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}