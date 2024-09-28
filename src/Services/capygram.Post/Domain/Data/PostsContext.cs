using capygram.Post.DependencyInjection.Options;
using capygram.Post.Domain.Model;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace capygram.Post.Domain.Data
{
    public class PostsContext : IPostsContext
    {
        public PostsContext(IOptionsMonitor<PostDBSetting> optionsMonitor ) {
            var postDBSetting = optionsMonitor.CurrentValue;
            var mongoClient = new MongoClient(postDBSetting.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(postDBSetting.DatabaseName);
            Posts = mongoDatabase.GetCollection<Posts>(postDBSetting.PostCollectionName);
        }
        public IMongoCollection<Posts> Posts { get ; set; }
    }
}
