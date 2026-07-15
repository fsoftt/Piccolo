using API.Common;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace API.Extensions
{
    public static class ResultExtensions
    {
        public static IActionResult ToActionResult<T>(this Result<T> result, ControllerBase controller)
        {
            if (result.IsSuccess)
            {
                return controller.Ok(result.Value);
            }

            return result.Error.Code switch
            {
                "Users.InvalidCredentials" => controller.Unauthorized(
                    new ApiError(
                        result.Error.Code, 
                        result.Error.Description)),
                _ => controller.BadRequest(
                    new ApiError(
                        result.Error.Code,
                        result.Error.Description)),
            };
        }
        public static IActionResult ToProblem(this Result result, ControllerBase controller)
        {
            var statusCode = result.Error.Code switch
            {
                "Users.InvalidCredentials" => 401,
                "Users.EmailAlreadyExists" => 409,
                _ => 400,
            };

            return controller.Problem(
                statusCode: statusCode,
                title: result.Error.Code,
                detail: result.Error.Description);
        }
    }
}
