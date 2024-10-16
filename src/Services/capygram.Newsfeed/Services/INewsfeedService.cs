using capygram.Common.DTOs.Post;
using capygram.Common.DTOs.Share;
using capygram.Common.Shared;

namespace capygram.Newsfeed.Services
{
    public interface INewsfeedService
    {
        public Task<Result<string>> AddFeedAsync(PostCreatedDTO post);
        public Task<Result<PaginationDTO<PostDBDTO>>> GetNewsfeedsByUserId(Guid UserId, int limit);
    }
}
