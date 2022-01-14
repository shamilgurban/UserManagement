using MediatR;

namespace Consumer.API.Services.UserService.Commands
{
    public class UserCreateCommand : IRequest
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FatherName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
    }
}
