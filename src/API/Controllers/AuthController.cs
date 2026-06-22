using API.Contracts.Authentication;
using API.Extensions;
using Azure.Core;
using Business.Common;
using Business.Users.Commands.LoginUser;
using Business.Users.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly ISender sender;

        public AuthController(ISender sender)
        {
            this.sender = sender;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            var command = new RegisterUserCommand(request.Email, request.Password);

            Result<Guid> result = await sender.Send(command);
            if (result.IsFailure)
            {
                return result.ToProblem(this);
            }

            return Ok(new RegisterResponse(result.Value));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            Result<string> result = await sender.Send(command);
            if (result.IsFailure)
            {
                return result.ToProblem(this);
            }

            return Ok(new LoginResponse(result.Value!));
        }
    }
}
