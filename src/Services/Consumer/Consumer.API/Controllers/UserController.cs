using AutoMapper;
using Consumer.API.Models;
using Consumer.API.Models.Dtos.User;
using Consumer.API.Services.UserService.Commands;
using Consumer.API.Services.UserService.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Consumer.API.Controllers
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
        public async Task<IActionResult> BindUserToOrganisation([FromBody] UserUpdateByOrganisationDto userUpdateByOrganisationDto)
        {
            await mediator.Send(mapper.Map<UserOrganisationBindCommand>(userUpdateByOrganisationDto));
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUsersByOrganisationId([FromQuery] UserByOrganisationIdDto userByOrganisationIdDto)
        {
            PaginatedList<UserDto> users = await mediator.Send(mapper.Map<UserListByOrganisationIdQuery>(userByOrganisationIdDto));
            return Ok(users);
        }
    }
}
