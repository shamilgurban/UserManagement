using Consumer.API.Models;
using Consumer.API.Models.Dtos.User;
using MediatR;

namespace Consumer.API.Services.UserService.Queries
{
    public class UserListByOrganisationIdQuery : BaseQuery, IRequest<PaginatedList<UserDto>>
    {
        public int OrganisationId { get; set; }
    }
}
