using AutoMapper;
using eSlozka.Core.Commands.Users;
using eSlozka.Domain.DataTransferObjects.Forms;
using eSlozka.Domain.DataTransferObjects.Sessions;
using eSlozka.Domain.Models;

namespace eSlozka.Application.Mappings;

public class UserMappingConfiguration : Profile
{
    public UserMappingConfiguration()
    {
        CreateMap<User, User>().ForMember(member => member.Roles, options => options.Ignore())
            .ForMember(member => member.Folders, options => options.Ignore())
            .ForMember(member => member.UserCreatedBy, options => options.Ignore())
            .ForMember(member => member.UserUpdatedBy, options => options.Ignore());

        CreateMap<User, RegisterCommand>()
            .ForMember(member => member.Password, options => options.Ignore())
            .ReverseMap()
            .ForMember(member => member.Password, options => options.Ignore());

        CreateMap<User, VerifyCredentialsCommand>()
            .ForMember(member => member.Password, options => options.Ignore())
            .ReverseMap()
            .ForMember(member => member.Password, options => options.Ignore());

        CreateMap<RegisterForm, RegisterCommand>().ReverseMap();
        CreateMap<LoginForm, VerifyCredentialsCommand>().ReverseMap();
        CreateMap<User, UserSession>();
    }
}