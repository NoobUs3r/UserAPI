using MediatR;
using Microsoft.AspNetCore.Mvc;
using UserAPI.Commands;
using UserAPI.Models;
using UserAPI.Queries;

namespace UserAPI.Controllers.V1
{
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ResponseList<User>> Get(bool useCache)
        {
            return await _mediator.Send(new GetAllUsersQuery(useCache));
        }

        [HttpGet("id")]
        [ProducesResponseType(typeof(Response<User>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id, bool useCache)
        {
            Response<User> response = await _mediator.Send(new GetUserByIdQuery(id, useCache));
            return response.Data == null ? NotFound() : Ok(response);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create(User user)
        {
            Response<User> response = await _mediator.Send(new CreateUserCommand(user));
            return response.Data == null ? BadRequest() : Ok(response.Data);
        }

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Update(int id, User user)
        {
            int response = await _mediator.Send(new UpdateUserCommand(id, user));
            return response == 0 ? BadRequest() : NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(int id)
        {
            int response = await _mediator.Send(new DeleteUserCommand(id));
            return response == 0 ? NotFound() : NoContent();
        }
    }
}
