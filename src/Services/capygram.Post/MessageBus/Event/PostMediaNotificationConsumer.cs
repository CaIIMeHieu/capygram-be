using capygram.Common.Abstraction;
using capygram.Common.MessageBus.Events;
using MediatR;

namespace capygram.Post.MessageBus.Event
{
    public class PostMediaNotificationConsumer : Consumer<ImageUploadedNotification>
    {
        public PostMediaNotificationConsumer(ISender sender) : base(sender)
        {
        }
    }
}
