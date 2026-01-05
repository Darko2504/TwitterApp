using AutoMapper;
using TwitterApp.Domain.Entities;
using TwitterApp.Dtos;
using TwitterApp.Dtos.UserDtos;

namespace TwitterApp.Mappers
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Post, PostDto>()
    .ForMember(dest => dest.Username,
        opt => opt.MapFrom(src => src.User.UserName))
    .ForMember(dest => dest.UserId,
        opt => opt.MapFrom(src => src.User.Id)) 
    .ForMember(dest => dest.LikesCount,
        opt => opt.MapFrom(src => src.Likes.Count))
    .ForMember(dest => dest.IsRetweet,
        opt => opt.MapFrom(src => src.RetweetOfPostId != null))
    .ForMember(dest => dest.OriginalContent, opt => opt.MapFrom(src => src.RetweetOfPost != null ? src.RetweetOfPost.Content : null))
    .ForMember(dest => dest.OriginalUsername, opt => opt.MapFrom(src => src.RetweetOfPost != null ? src.RetweetOfPost.User.UserName : null));


            CreateMap<CreatePostDto, Post>();

            CreateMap<User, UserProfileDto>()
                .ForMember(dest => dest.Username,
                    opt => opt.MapFrom(src => src.UserName));
        }
    }
}
