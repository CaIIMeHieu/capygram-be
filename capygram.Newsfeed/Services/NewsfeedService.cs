using capygram.Common.DTOs.Post;
using capygram.Common.DTOs.Share;
using capygram.Common.Shared;
using capygram.Newsfeed.Domain.Modal;
using capygram.Newsfeed.Domain.Repositories;

namespace capygram.Newsfeed.Services
{
    public class NewsfeedService : INewsfeedService
    {
        private readonly INewsfeedRepositories _newsfeedRepositories;
        public NewsfeedService( INewsfeedRepositories newsfeedRepositories ) { 
            _newsfeedRepositories = newsfeedRepositories;
        }
        public async Task<Result<string>> AddFeedAsync(PostCreatedDTO post)
        {
            var feed = new capygram.Newsfeed.Domain.Modal.Newsfeed
            {
                Id = new Guid(),
                CreatedAt = DateTime.Now,
                PostId = post.PostId,
                UserId = post.UserId
            };
            await _newsfeedRepositories.AddNewsfeed(feed);
            return Result<string>.CreateResult(true, new ResultDetail("200", "Success"), "Add feed successfully");
        }

        public async Task<Result<PaginationDTO<PostDBDTO>>> GetNewsfeedsByUserId(Guid UserId,int limit)
        {
            var posts = await _newsfeedRepositories.GetNewsfeedsByUserId(UserId,limit);
            return Result<PaginationDTO<PostDBDTO>>.CreateResult(true, new ResultDetail("200", "Success"), posts);
        }
    }
}
