using Microsoft.Extensions.DependencyInjection;
using TwitterApp.DataAcess.Repository.Abstractions;
using TwitterApp.DataAcess.Repository.Implementations;

namespace TwitterApp.Helpers.DIContainer
{
    public class DIHelper
    {
        public static void InjectDbRepositories(IServiceCollection services)
        {
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<IPostLikeRepository, PostLikeRepository>();

        }
    }
}
