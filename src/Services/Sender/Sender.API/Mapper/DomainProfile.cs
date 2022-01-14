using AutoMapper;
using Sender.API.Models.Users;
using Sender.API.Services.UserService.Commands;
using Shared.Models;

namespace Sender.API.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<UserCreateDto, UserCreateCommand>();
            CreateMap<UserCreateCommand, User>();
        }
    }
}
