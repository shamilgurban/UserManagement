using AutoMapper;
using Consumer.API.Services.UserService.Commands;
using MassTransit;
using MediatR;
using Shared.Models;
using System;
using System.Threading.Tasks;

namespace Consumer.API.Consumers
{
    public class UserConsumer : IConsumer<User>
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UserConsumer(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }


        public async Task Consume(ConsumeContext<User> context)
        {
            try
            {
                await mediator.Send(mapper.Map<UserCreateCommand>(context.Message));
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("Message can not be processed: " + ex.Message);
            }
        }
    }
}
