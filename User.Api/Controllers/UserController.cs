using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User.Api.Application.Core.Application.Users.Command;
using User.Api.Application.Core.Application.Users.Queries;

namespace User.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetList()
        {
            var res = await _mediator.Send(new UserGetListQry());
            return StatusCode((int)res.Code, res);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] UserCreateCmd command)
        {
            var res = await _mediator.Send(command);
            return StatusCode((int)res.Code, res);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginCmd command)
        {
            var res = await _mediator.Send(command);
            return StatusCode((int)res.Code, res);
        }

        [HttpGet("find")]
        public async Task<IActionResult> Find([FromQuery] UserFindQry query)
        {
            var res = await _mediator.Send(query);
            return StatusCode((int)res.Code, res);
        }
    }
}
