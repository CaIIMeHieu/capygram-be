using capygram.Common.DTOs.Post;
using capygram.Common.DTOs.Share;
using capygram.Newsfeed.Domain.Modal;

namespace capygram.Newsfeed.Domain.Repositories
{
    public interface INewsfeedRepositories
    {
        public Task AddNewsfeed(capygram.Newsfeed.Domain.Modal.Newsfeed feed);
        public Task<PaginationDTO<PostDBDTO>> GetNewsfeedsByUserId(Guid UserId, int limit);
    }
}
