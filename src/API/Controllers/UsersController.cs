using API.Extensions;
using Business.Common;
using Business.Users.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UsersController : ControllerBase
    {
        private readonly ISender sender;

        public UsersController(ISender sender)
        {
            this.sender = sender;
        }

        [Authorize]
        [HttpGet("me")]
        public async Task<IActionResult> Me()
        {
            Result<GetCurrentUserResponse> result = await sender.Send(new GetCurrentUserQuery());
            if (result.IsFailure)
            {
                return result.ToProblem(this);
            }

            return Ok(result.Value);
        }
    }
}
