using API.Contracts.Authentication;
using Azure.Core;
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

            Guid userId = await sender.Send(command);

            return Ok(new
            {
                UserId = userId
            });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            var command = new LoginUserCommand(request.Email, request.Password);

            string token = await sender.Send(command);

            return Ok(new
            {
                AccessToken = token
            });
        }
    }
}
