using capygram.Post.Domain.Model;

namespace capygram.Post.Domain.Repositories
{
    public interface IPostRepositories
    {
        public Task<List<Posts>> GetPostsAsync(int pageSize, int pageNumber);
        public Task<Posts> GetPostByIdAsync(Guid PostID);
        public Task CreatePostAsync(Posts newPosts );
        public Task UpdatePostByIdAsync(Posts post);
        public Task DeletePostByIdAsync( Guid PostID );
        public Task<long> GetTotalPosts();

        public Task<List<Posts>> GetPostByUserIdAsync(Guid UserID);
    }
}
