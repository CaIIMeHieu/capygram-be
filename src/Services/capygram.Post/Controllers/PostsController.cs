using capygram.Common.DTOs.Post;
using capygram.Post.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace capygram.Post.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        [HttpPost]
        [Route("Create")]
        public async Task<IActionResult> CreatePost([FromForm] PostDTO newPost)
        {
            var result = await _postService.CreatePostAsync(newPost);
            return Ok(result);
        }

        [HttpDelete]
        [Route("Delete/{postId}")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            var result = await _postService.DeletePostByIdAsync(postId);
            return Ok(result);
        }

        [HttpGet]
        [Route("Get/{postId}")]
        public async Task<IActionResult> GetPostById(Guid postId)
        {
            var result = await _postService.GetPostByIdAsync(postId);
            return Ok(result);
        }

        [HttpGet("GetPostsByUserID")]
        public async Task<IActionResult> GetPostByUserId(Guid UserId)
        {
            var result = await _postService.GetPostByUserIdAsync(UserId);
            return Ok(result);
        }

        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetPosts([FromQuery]int pageSize, int pageNumber)
        {
            var result = await _postService.GetPostsAsync(pageSize, pageNumber);
            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdatePost( [FromBody] PostUpdateDTO post)
        {
            var result = await _postService.UpdatePostByIdAsync(post);
            return Ok(result);
        }
    }
}
