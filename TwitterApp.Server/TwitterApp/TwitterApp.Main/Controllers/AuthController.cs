using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Controllers;
using TwitterApp.Dtos.UserDtos;
using TwitterApp.Services.UserService.Abstractions;
using TwitterApp.Shared.CustomExceptions;
using TwitterApp.Shared.CustomExceptions.UserExceptions;
using TwitterApp.Shared.Responses;

namespace TwitterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize]
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

        [Authorize]
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


        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserRequestDto request)
        {
            try
            {
                var response = await _userService.RegisterUserAsync(request);
                return Response(response);
            }
            catch (UserDataException ex)
            {
                return Response(new CustomResponse(ex.Message));
            }
            catch (Exception)
            {
                return Response(new CustomResponse("Unexpected error occurred during registration."));
            }
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequestDto request)
        {
            try
            {
                var response = await _userService.LoginUserAsync(request);
                return Response(response);
            }
            catch (UserNotFoundException ex)
            {
                return Response(new CustomResponse(ex.Message));
            }
            catch (InvalidCredentialsException ex)
            {
                return Response(new CustomResponse(ex.Message));
            }
            catch (Exception)
            {
                return Response(new CustomResponse("Unexpected error occurred during login."));
            }
        }
    }
}
