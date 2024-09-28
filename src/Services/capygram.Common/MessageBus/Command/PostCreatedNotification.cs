using capygram.Common.Abstraction;
using capygram.Common.DTOs.Post;

namespace capygram.Common.MessageBus.Command
{
    public class PostCreatedNotification : INotification
    {
        public Guid Id { get; set; }
        //mảng mảng byte
        public PostCreatedDTO Data { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
    }
}
