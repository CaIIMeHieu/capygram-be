using capygram.Common.Abstraction;
using capygram.Common.MessageBus.Events;
using MediatR;

namespace capygram.Media.MessageBus.Event
{
    public class ImageUploadConsumer : Consumer<ImageUploadNotification>
    {
        public ImageUploadConsumer(ISender sender) : base(sender)
        {

        }
    }
}
