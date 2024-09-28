using MongoDB.Bson.Serialization.Attributes;

namespace capygram.Post.Domain.Model
{
    public class Posts
    {
        [BsonId]
        public Guid Id { get; set; }
        public List<string> ImageUrls { get; set; }
        public int Likes { get; set; }
        public string UserName { get; set; }   
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdateAt { get; set; }

    }
}
