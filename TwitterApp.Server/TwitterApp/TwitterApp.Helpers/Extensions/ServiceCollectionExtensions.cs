using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TwitterApp.DataAcess.TwitterAppDbContext;

namespace TwitterApp.Helpers.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public class ConfigBuilder
        {
            public IServiceCollection Services { get; set; }
            public IConfiguration Configuration { get; set; }
            public IdentityBuilder Identity { get; set; }
            public AuthenticationBuilder AuthenticationBuilder { get; set; }

            public ConfigBuilder(IServiceCollection services, IConfiguration configuration)
            {
                Services = services;
                Configuration = configuration;
            }
        }
        public static ConfigBuilder AddPostgreSqlDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<TwitterAppDbContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });
            return new(services, configuration);
        }
    }
}
