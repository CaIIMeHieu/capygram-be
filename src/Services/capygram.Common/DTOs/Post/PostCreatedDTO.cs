namespace capygram.Common.DTOs.Post
{
    public class PostCreatedDTO 
    {
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
