using capygram.Post.Domain.Data;
using capygram.Post.Domain.Model;
using capygram.Post.Domain.Repositories;
using MongoDB.Driver;

namespace capygram.Post.Repositories
{
    public class PostRepositories : IPostRepositories
    {
        private readonly IPostsContext _context;
        public PostRepositories(IPostsContext context)
        {
            _context = context;
        }
        public async Task CreatePostAsync(Posts newPosts)
        {
            await _context.Posts.InsertOneAsync(newPosts);
        }

        public async Task DeletePostByIdAsync(Guid PostID)
        {
            await _context.Posts.DeleteOneAsync( Post => Post.Id == PostID );
        }

        public async Task<Posts> GetPostByIdAsync(Guid PostID)
        {
            var post = await _context.Posts.Find(Post => Post.Id == PostID).FirstOrDefaultAsync();
            return post;
        }

        public async Task<List<Posts>> GetPostByUserIdAsync(Guid UserID)
        {
            var posts = await _context.Posts.Find(Post => Post.UserId == UserID).ToListAsync();
            return posts;
        }

        public async Task<List<Posts>> GetPostsAsync( int pageSize , int pageNumber )
        {
            var posts = await _context.Posts.Find(_ => true).Skip(pageSize * (pageNumber - 1)).Limit(pageSize).ToListAsync();
            return posts;
        }

        public async Task<long> GetTotalPosts()
        {
            var total = await _context.Posts.CountDocumentsAsync(Builders<Posts>.Filter.Empty);
            return total;
        }

        public async Task UpdatePostByIdAsync(Posts post)
        {
            await _context.Posts.ReplaceOneAsync(Post => Post.Id == post.Id, post);
        }
    }
}
