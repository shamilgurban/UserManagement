using AutoMapper;
using Consumer.API.DataAccess.Users;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Consumer.API.Services.UserService.Commands
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand>
    {
        private readonly IUserRepository userRepository;
        private readonly IMapper mapper;
        private readonly ILogger logger;

        public UserCreateCommandHandler(IUserRepository userRepository, IMapper mapper, ILogger logger)
        {
            this.userRepository = userRepository;
            this.mapper = mapper;
            this.logger = logger;
        }


        public async Task<Unit> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = mapper.Map<User>(request);
                await userRepository.Insert(user);
                await userRepository.Save();
                return Unit.Value;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw new Exception("Error happened while creating user.");
            }
        }
    }
}
