using MongoDB.Driver;
using capygram.Post.Domain.Model;

namespace capygram.Post.Domain.Data
{
    public interface IPostsContext
    {
        public IMongoCollection<Posts> Posts { get; set; }
    }
}
