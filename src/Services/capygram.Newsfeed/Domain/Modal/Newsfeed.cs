namespace capygram.Newsfeed.Domain.Modal
{
    public class Newsfeed
    {
        public Guid Id { get; set; }
        public Guid PostId { get; set; }
        public Guid UserId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
