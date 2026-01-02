using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace TwitterApp.Domain.Entities
{
    public class User : IdentityUser
    {
        [JsonIgnore]
        public List<Post> Posts { get; set; } = new List<Post>();

        [JsonIgnore]
        public List<PostLike> Likes { get; set; } = new List<PostLike>();
    }
}
