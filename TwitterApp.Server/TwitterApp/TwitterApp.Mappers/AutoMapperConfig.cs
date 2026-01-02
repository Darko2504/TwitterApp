using AutoMapper;
using TwitterApp.Domain.Entities;
using TwitterApp.Dtos;

namespace TwitterApp.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Post mappings
            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.LikesCount,
                    opt => opt.MapFrom(src => src.Likes.Count))
                .ForMember(dest => dest.IsRetweet,
                    opt => opt.MapFrom(src => src.RetweetOfPostId != null));

            CreateMap<CreatePostDto, Post>();

            // User profile mappings
            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.UserName));
        }
    }
}
