using Microsoft.Extensions.DependencyInjection;
using TwitterApp.DataAcess.Repository.Abstractions;
using TwitterApp.DataAcess.Repository.Implementations;
using TwitterApp.Services.Abstractions;
using TwitterApp.Services.Implementations;
using TwitterApp.Services.UserService.Abstractions;
using TwitterApp.Services.UserService.Implementations;

namespace TwitterApp.Helpers.DIContainer
{
    public static class DIHelper
    {
        public static void InjectDbRepositories(IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostLikeRepository, PostLikeRepository>();

        }
        public static void InjectServices(IServiceCollection services)
        {
            services.AddTransient<ITokenService, TokenService>();
            services.AddTransient<IPostService, PostService>();
            services.AddTransient<IUserService, UserService>();

        }
    }
}
