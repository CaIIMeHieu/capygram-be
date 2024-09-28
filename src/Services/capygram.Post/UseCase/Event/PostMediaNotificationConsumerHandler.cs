using capygram.Common.DTOs.Post;
using capygram.Common.MessageBus.Events;
using capygram.Post.Domain.Model;
using capygram.Post.Domain.Repositories;
using capygram.Post.Services;
using MediatR;

namespace capygram.Post.UseCase.Event
{
    public class PostMediaNotificationConsumerHandler : IRequestHandler<ImageUploadedNotification>
    {
        private readonly IPostRepositories _postRepository;
        public PostMediaNotificationConsumerHandler(IPostRepositories postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task Handle(ImageUploadedNotification request, CancellationToken cancellationToken)
        {
            var post = await _postRepository.GetPostByIdAsync(request.Id);
            post.ImageUrls = request.urlUpload;
            await _postRepository.UpdatePostByIdAsync( post );
        }
    }
}
