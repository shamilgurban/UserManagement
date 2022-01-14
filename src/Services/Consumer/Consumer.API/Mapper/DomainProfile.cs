using AutoMapper;
using Consumer.API.DataAccess.Organisations;
using Consumer.API.DataAccess.Users;
using Consumer.API.Models.Dtos;
using Consumer.API.Models.Dtos.Organisation;
using Consumer.API.Models.Dtos.User;
using Consumer.API.Services.UserService.Commands;
using Consumer.API.Services.UserService.Queries;

namespace Consumer.API.Mapper
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Shared.Models.User, UserCreateCommand>();
            CreateMap<BaseDto, BaseQuery>();
            CreateMap<UserCreateCommand, User>();
            CreateMap<UserUpdateByOrganisationDto, UserOrganisationBindCommand>();
            CreateMap<User, UserDto>();
            CreateMap<Organisation, OrganisationDto>();
            CreateMap<UserByOrganisationIdDto, UserListByOrganisationIdQuery>();
        }
    }
}
