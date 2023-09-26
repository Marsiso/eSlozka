using AutoMapper;
using eSlozka.Domain.Models;

namespace eSlozka.Application.Mappings;

public class CodeListItemMappingConfiguration : Profile
{
    public CodeListItemMappingConfiguration()
    {
        CreateMap<CodeListItem, CodeListItem>().ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());
    }
}