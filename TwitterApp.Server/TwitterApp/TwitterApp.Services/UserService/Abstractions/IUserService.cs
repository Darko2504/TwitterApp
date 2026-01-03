using TwitterApp.Dtos.UserDtos;
using TwitterApp.Shared.Responses;

namespace TwitterApp.Services.UserService.Abstractions
{
    public interface IUserService
    {
        Task<CustomResponse<RegisterUserResponseDto>> RegisterUserAsync(RegisterUserRequestDto request);
        Task<CustomResponse<LoginUserResponseDto>> LoginUserAsync(LoginUserRequestDto request);
        Task<CustomResponse<UserDto>> GetUserByIdAsync(string id);
    }
}
