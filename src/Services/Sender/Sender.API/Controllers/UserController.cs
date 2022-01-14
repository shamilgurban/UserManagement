using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Sender.API.Models.Users;
using Sender.API.Services.UserService.Commands;
using System.Threading.Tasks;

namespace Sender.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        private readonly IMapper mapper;

        public UserController(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateDto userCreateDto)
        {
            await mediator.Send(mapper.Map<UserCreateCommand>(userCreateDto));
            return Ok();
        }
    }
}
