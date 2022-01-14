using Consumer.API.DataAccess.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer.API.Services.UserService.Commands
{
    public class UserOrganisationBindCommandHandler : IRequestHandler<UserOrganisationBindCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly ILogger logger;

        public UserOrganisationBindCommandHandler(IUserRepository userRepository, ILogger logger)
        {
            this.userRepository = userRepository;
            this.logger = logger;
        }

        public async Task<Unit> Handle(UserOrganisationBindCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = await userRepository.GetById(request.UserId);
                user.OrganisationId = request.OrganisationId;
                await userRepository.Update(user);
                await userRepository.Save();
                return Unit.Value;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception("Error happened while binding user to organisation.");
            }
        }
    }
}
