using capygram.Common.Abstraction;
using capygram.Common.MessageBus.Command;
using MediatR;

namespace capygram.Newsfeed.MessageBus.Command
{
    public class PostCreatedConsumer : Consumer<PostCreatedNotification>
    {
        public PostCreatedConsumer(ISender sender) : base(sender)
        {
        }
    }
}
