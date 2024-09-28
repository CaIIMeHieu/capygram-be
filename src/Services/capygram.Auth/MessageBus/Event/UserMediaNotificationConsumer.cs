using capygram.Common.Abstraction;
using capygram.Common.MessageBus.Events;
using MediatR;

namespace capygram.Auth.MessageBus.Event
{
    public class UserMediaNotificationConsumer : Consumer<ImageUploadedNotification>
    {
        public UserMediaNotificationConsumer(ISender sender) : base(sender)
        {
        }
    }
}
