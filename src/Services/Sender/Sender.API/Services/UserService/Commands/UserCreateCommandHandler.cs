using AutoMapper;
using MassTransit;
using MediatR;
using Microsoft.Extensions.Logging;
using Shared.Models;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Sender.API.Services.UserService.Commands
{
    public class UserCreateCommandHandler : IRequestHandler<UserCreateCommand>
    {
        private readonly IMapper mapper;
        private readonly IPublishEndpoint publishEndPoint;
        private readonly ILogger logger;

        public UserCreateCommandHandler(IMapper mapper, IPublishEndpoint publishEndPoint, ILogger logger)
        {
            this.mapper = mapper;
            this.publishEndPoint = publishEndPoint;
            this.logger = logger;
        }


        public async Task<Unit> Handle(UserCreateCommand request, CancellationToken cancellationToken)
        {
            try
            {
                User user = mapper.Map<User>(request);
                await publishEndPoint.Publish(user);

                Console.Out.WriteLine("Following data has sent to RabbitMQ: "
                    + user.Name + " "
                    + user.Surname + " "
                    + user.FatherName + " "
                    + user.Email + " "
                    + user.PhoneNumber);

                return Unit.Value;
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine(ex.Message);
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}