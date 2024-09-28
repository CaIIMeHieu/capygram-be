using capygram.Common.DTOs.Post;
using capygram.Common.DTOs.Share;
using capygram.Common.Shared;
using capygram.Post.Domain.Model;

namespace capygram.Post.Services
{
    public interface IPostService
    {
        public Task<Result<PaginationDTO<Posts>>> GetPostsAsync(int pageSize, int pageNumber);
        public Task<Result<Posts>> GetPostByIdAsync(Guid PostID);
        public Task<Result<string>> CreatePostAsync(PostDTO newPosts);
        public Task<Result<string>> UpdatePostByIdAsync(PostUpdateDTO post);
        public Task<Result<string>> DeletePostByIdAsync(Guid PostID);

        public Task<Result<List<Posts>>> GetPostByUserIdAsync(Guid UserID);
    }
}
