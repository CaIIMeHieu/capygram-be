using capygram.Common.Abstraction;

namespace capygram.Common.MessageBus.Events
{
    public class ImageUploadNotification : INotification
    {
        public Guid Id { get; set; }
        //mảng mảng byte
        public List<byte[]> Data { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public DateTimeOffset TimeStamp { get; set; } = DateTimeOffset.Now;
        public string CloudinaryFolder { get; set; }
    }
}
