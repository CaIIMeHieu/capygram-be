namespace capygram.Common.DTOs.Post
{
    public class PostDTO
    {
        public List<IFormFile> ImageUrls { get; set; }
        public int Likes { get; set; }
        public string UserName { get; set; }
        public string Content { get; set; }
        public Guid UserId { get; set; }
        public string UserAvartar { get; set; }
    }
}
