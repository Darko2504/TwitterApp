using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TwitterApp.Domain.Entities;
using TwitterApp.Dtos;
using TwitterApp.Dtos.UserDtos;
using TwitterApp.Services.UserService.Abstractions;
using TwitterApp.Shared.CustomExceptions.UserExceptions;
using TwitterApp.Shared.Responses;

namespace TwitterApp.Services.UserService.Implementations
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        public UserService(UserManager<User> userManager, ITokenService tokenService, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        public async Task<CustomResponse<RegisterUserResponseDto>> RegisterUserAsync(RegisterUserRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username))
                    throw new UserDataException("Username is required.");
                if (string.IsNullOrWhiteSpace(request.Password))
                    throw new UserDataException("Password is required.");
                if (string.IsNullOrWhiteSpace(request.Email))
                    throw new UserDataException("Email is required.");

                var existingUser = await _userManager.FindByNameAsync(request.Username);
                if (existingUser != null)
                    throw new UserDataException("Username already exists.");

                var user = new User
                {
                    UserName = request.Username,
                    Email = request.Email
                };

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded)
                    return new CustomResponse<RegisterUserResponseDto>(
                        result.Errors.Select(e => e.Description).ToList()
                    );

                var responseDto = new RegisterUserResponseDto
                {
                    Id = user.Id,
                    Username = user.UserName,
                    Email = user.Email
                };

                return new CustomResponse<RegisterUserResponseDto>(responseDto);
            }
            catch (UserDataException ex)
            {
                return new CustomResponse<RegisterUserResponseDto>(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse<RegisterUserResponseDto>($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<CustomResponse<LoginUserResponseDto>> LoginUserAsync(LoginUserRequestDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Username))
                    throw new UserDataException("Username is required.");
                if (string.IsNullOrWhiteSpace(request.Password))
                    throw new UserDataException("Password is required.");

                var user = await _userManager.FindByNameAsync(request.Username);
                if (user == null)
                    throw new UserNotFoundException("User does not exist.");

                var isValid = await _userManager.CheckPasswordAsync(user, request.Password);
                if (!isValid)
                    throw new InvalidCredentialsException("Invalid password.");

                var tokenResponse = await _tokenService.GenerateTokenAsync(user);

                var responseDto = new LoginUserResponseDto
                {
                    Token = tokenResponse.Token,
                    ValidTo = tokenResponse.ValidTo
                };

                return new CustomResponse<LoginUserResponseDto>(responseDto);
            }
            catch (UserNotFoundException ex)
            {
                return new CustomResponse<LoginUserResponseDto>(ex.Message);
            }
            catch (InvalidCredentialsException ex)
            {
                return new CustomResponse<LoginUserResponseDto>(ex.Message);
            }
            catch (UserDataException ex)
            {
                return new CustomResponse<LoginUserResponseDto>(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse<LoginUserResponseDto>($"Unexpected error: {ex.Message}");
            }
        }
        public async Task<CustomResponse<UserDto>> GetUserByIdAsync(string id)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(id);
                if (user == null)
                    throw new UserNotFoundException("User not found.");

                var userDto = _mapper.Map<UserDto>(user);
                return new CustomResponse<UserDto>(userDto);
            }
            catch (UserNotFoundException ex)
            {
                return new CustomResponse<UserDto>(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse<UserDto>($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<CustomResponse<UserProfileDto>> GetUserProfileAsync(string userId)
        {
            try
            {
                var user = await _userManager.Users
     .Include(u => u.Posts) 
     .FirstOrDefaultAsync(u => u.Id == userId);
                if (user == null)
                    throw new UserNotFoundException("User not found.");

                var profile = new UserProfileDto
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Posts = user.Posts != null
                        ? _mapper.Map<List<PostDto>>(user.Posts)
                        : new List<PostDto>()
                };

               
                return new CustomResponse<UserProfileDto>(profile);
            }
            catch (UserNotFoundException ex)
            {
                return new CustomResponse<UserProfileDto>(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse<UserProfileDto>($"Unexpected error: {ex.Message}");
            }
        }
    }
}
