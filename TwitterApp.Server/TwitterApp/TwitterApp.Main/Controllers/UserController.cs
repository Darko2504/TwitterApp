using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Controllers;
using TwitterApp.Dtos.UserDtos;
using TwitterApp.Services.UserService.Abstractions;
using TwitterApp.Shared.CustomExceptions.UserExceptions;
using TwitterApp.Shared.Responses;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController : BaseController
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUserById(string id)
    {
        try
        {
            var response = await _userService.GetUserByIdAsync(id);
            return Response(response);
        }
        catch (UserNotFoundException ex)
        {
            return Response(new CustomResponse<UserDto>(ex.Message));
        }
        catch (Exception ex)
        {
            return Response(new CustomResponse<UserDto>($"Unexpected error: {ex.Message}"));
        }
    }
    [HttpGet("profile/{userId}")]
    public async Task<IActionResult> GetProfile(string userId)
    {
        try
        {
            var response = await _userService.GetUserProfileAsync(userId);
            return Response(response);
        }
        catch (UserNotFoundException ex)
        {
            return Response(new CustomResponse<UserProfileDto>(ex.Message));
        }
        catch (Exception ex)
        {
            return Response(new CustomResponse<UserProfileDto>($"Unexpected error: {ex.Message}"));
        }
    }

}
