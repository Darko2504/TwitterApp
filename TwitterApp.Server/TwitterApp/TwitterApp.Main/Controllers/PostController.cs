using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PizzaApp.Controllers;
using System.Security.Claims;
using TwitterApp.Dtos;
using TwitterApp.Services.Abstractions;
using TwitterApp.Shared.Responses;

namespace TwitterApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class PostController : BaseController
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpGet("feed")]
        public async Task<IActionResult> GetFeed()
        {
            try
            {
                var response = await _postService.GetFeedAsync();
                return Response(response);
            }
            catch (Exception ex)
            {
                return Response(new CustomResponse($"Unexpected error: {ex.Message}"));
            }
        }

        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetPostsByUser(string userId)
        {
            try
            {
                var response = await _postService.GetUserPostsAsync(userId);
                return Response(response);
            }
            catch (Exception ex)
            {
                return Response(new CustomResponse($"Unexpected error: {ex.Message}"));
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] CreatePostDto request)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var response = await _postService.CreatePostAsync(request, userId);
                return Response(response);
            }
            catch (Exception ex)
            {
                return Response(new CustomResponse($"Unexpected error: {ex.Message}"));
            }
        }

        [HttpPost("{postId}/like")]
        public async Task<IActionResult> LikePost(int postId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var response = await _postService.LikePostAsync(postId, userId);
                return Response(response);
            }
            catch (Exception ex)
            {
                return Response(new CustomResponse($"Unexpected error: {ex.Message}"));
            }
        }

        [HttpPost("{postId}/unlike")]
        public async Task<IActionResult> UnlikePost(int postId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                var response = await _postService.UnlikePostAsync(postId, userId);
                return Response(response);
            }
            catch (Exception ex)
            {
                return Response(new CustomResponse($"Unexpected error: {ex.Message}"));
            }
        }

        [HttpPost("{postId}/retweet")]
        public async Task<IActionResult> RetweetPost(int postId)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                var dto = new CreatePostDto
                {
                    Content = null,
                    RetweetOfPostId = postId
                };

                var response = await _postService.CreatePostAsync(dto, userId);
                return Response(response);
            }
            catch (Exception ex)
            {
                return Response(new CustomResponse($"Unexpected error: {ex.Message}"));
            }
        }

       
    }
}
