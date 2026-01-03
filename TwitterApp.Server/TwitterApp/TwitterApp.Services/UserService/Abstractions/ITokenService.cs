using System.IdentityModel.Tokens.Jwt;
using TwitterApp.Domain.Entities;
using TwitterApp.Dtos;

namespace TwitterApp.Services.UserService.Abstractions
{
    public interface ITokenService
    {
        Task<TokenDto> GenerateTokenAsync(User user);
    }
}
