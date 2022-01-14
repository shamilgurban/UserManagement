using MediatR;

namespace Consumer.API.Services.UserService.Commands
{
    public class UserOrganisationBindCommand : IRequest
    {
        public int UserId { get; set; }
        public int OrganisationId { get; set; }
    }
}
