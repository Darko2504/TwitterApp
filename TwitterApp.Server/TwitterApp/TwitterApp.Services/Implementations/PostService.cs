using AutoMapper;
using TwitterApp.DataAcess.Repository.Abstractions;
using TwitterApp.Domain.Entities;
using TwitterApp.Dtos;
using TwitterApp.Services.Abstractions;
using TwitterApp.Shared.CustomExceptions.PostExceptions;
using TwitterApp.Shared.CustomExceptions.PostLikeExceptions;
using TwitterApp.Shared.Responses;

namespace TwitterApp.Services.Implementations
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostLikeRepository _postLikeRepository;
        private readonly IMapper _mapper;

        public PostService(
            IPostRepository postRepository,
            IPostLikeRepository postLikeRepository,
            IMapper mapper)
        {
            _postRepository = postRepository;
            _postLikeRepository = postLikeRepository;
            _mapper = mapper;
        }

        // Create a new post or retweet
        public async Task<CustomResponse<PostDto>> CreatePostAsync(CreatePostDto dto, string userId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(dto.Content) && dto.RetweetOfPostId == null)
                    throw new PostCreateException("Post content cannot be empty unless it is a retweet.");

                var post = _mapper.Map<Post>(dto);
                post.UserId = userId;

                await _postRepository.AddAsync(post);

                var postDto = _mapper.Map<PostDto>(post);
                return new CustomResponse<PostDto>(postDto);
            }
            catch (PostCreateException ex)
            {
                return new CustomResponse<PostDto>(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse<PostDto>($"Unexpected error: {ex.Message}");
            }
        }

        // Get all posts for the feed
        public async Task<CustomResponse<List<PostDto>>> GetFeedAsync(string currentUserId)
        {
            try
            {
                var posts = await _postRepository.GetFeedAsync();
                var postDtos = _mapper.Map<List<PostDto>>(posts);

                // mark liked posts for this user
                foreach (var postDto in postDtos)
                {
                    var post = posts.First(p => p.Id == postDto.Id);
                    postDto.IsLikedByCurrentUser = post.Likes.Any(l => l.UserId == currentUserId);
                }

                return new CustomResponse<List<PostDto>>(postDtos);
            }
            catch (PostNotFoundException ex)
            {
                return new CustomResponse<List<PostDto>>(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse<List<PostDto>>($"Unexpected error: {ex.Message}");
            }
        }

        // Get all posts by a specific user
        public async Task<CustomResponse<List<PostDto>>> GetUserPostsAsync(string userId, string currentUserId)
        {
            try
            {
                var posts = await _postRepository.GetPostsByUserAsync(userId);
                var postDtos = _mapper.Map<List<PostDto>>(posts);
                foreach (var postDto in postDtos)
                {
                    var post = posts.First(p => p.Id == postDto.Id);
                    postDto.IsLikedByCurrentUser =
                        post.Likes.Any(l => l.UserId == currentUserId);
                }



                return new CustomResponse<List<PostDto>>(postDtos);


            }
            catch (PostNotFoundException ex)
            {
                return new CustomResponse<List<PostDto>>(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse<List<PostDto>>($"Unexpected error: {ex.Message}");
            }
        }

        // Like a post
        public async Task<CustomResponse> LikePostAsync(int postId, string userId)
        {
            try
            {
                var exists = await _postLikeRepository.ExistsAsync(postId, userId);
                if (exists)
                    throw new PostAlreadyLikedException("User already liked this post.");

                var like = new PostLike
                {
                    PostId = postId,
                    UserId = userId
                };

                await _postLikeRepository.AddAsync(like);
                return new CustomResponse(true);
            }
            catch (PostAlreadyLikedException ex)
            {
                return new CustomResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse($"Unexpected error: {ex.Message}");
            }
        }

        // Unlike a post
        public async Task<CustomResponse> UnlikePostAsync(int postId, string userId)
        {

            try
            {
                var like = await _postLikeRepository.GetByPostAndUserAsync(postId, userId);
                if (like == null)
                    throw new PostLikeNotFoundException("Post like not found.");

                _postLikeRepository.Delete(like);
                await _postLikeRepository.SaveChangesAsync();
                return new CustomResponse(true);
            }
            catch (PostLikeNotFoundException ex)
            {
                return new CustomResponse(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse($"Unexpected error: {ex.Message}");
            }
        }

        public async Task<CustomResponse<PostDto>> RetweetPostAsync(int postId, string userId)
        {
            try
            {
                // Get the original post
                var originalPost = await _postRepository.GetByIdAsync(postId);
                if (originalPost == null)
                    throw new PostNotFoundException("Original post not found.");

                // Create new post as a retweet
                var retweet = new Post
                {
                    UserId = userId,
                    RetweetOfPostId = originalPost.Id,
                    Content = null // Retweet itself doesn't need new content
                };

                await _postRepository.AddAsync(retweet);

                var retweetDto = _mapper.Map<PostDto>(retweet);
                return new CustomResponse<PostDto>(retweetDto);
            }
            catch (PostNotFoundException ex)
            {
                return new CustomResponse<PostDto>(ex.Message);
            }
            catch (PostCreateException ex)
            {
                return new CustomResponse<PostDto>(ex.Message);
            }
            catch (Exception ex)
            {
                return new CustomResponse<PostDto>($"Unexpected error: {ex.Message}");
            }
        }
    }
}
