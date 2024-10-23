using capygram.Common.DTOs.Post;
using capygram.Common.DTOs.User;
using capygram.Common.MessageBus.Command;
using capygram.Common.Services;
using capygram.Common.Shared;
using capygram.Newsfeed.Services;
using MediatR;

namespace capygram.Newsfeed.UseCase.Command
{
    public class PostCreatedConsumerHandler : IRequestHandler<PostCreatedNotification>
    {
        private readonly INewsfeedService _newsfeedService;
        private readonly IExternalService _externalService;
        public PostCreatedConsumerHandler( INewsfeedService newsfeedService , IExternalService externalService  ) {
            _newsfeedService = newsfeedService;
            _externalService = externalService;
        }
        public async Task Handle(PostCreatedNotification request, CancellationToken cancellationToken)
        {
            var list_follower = await _externalService.GetExternalDataListAsync<UserChangedNotificationDto>($"capygram-graph:8085/api/{request.Data.UserId}/follower");
            if (list_follower != null )
            {
                foreach (var follower in list_follower)
                {
                    await _newsfeedService.AddFeedAsync(
                        new PostCreatedDTO
                        {
                            CreatedAt = request.Data.CreatedAt,
                            PostId = request.Data.PostId,
                            UserId = follower.Id,
                        }
                    );
                }
            }    
        }
    }
}
