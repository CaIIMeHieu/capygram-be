using capygram.Common.MessageBus.Events;
using capygram.Media.Service;
using MassTransit;
using MediatR;

namespace capygram.Media.Usecase.Event
{
    public class ImageUploadConsumerHandler : IRequestHandler<ImageUploadNotification>
    {
        private IPublishEndpoint _publishEndpoint;
        private IMediaService _mediaService;
        public ImageUploadConsumerHandler( IPublishEndpoint publishEndpoint , IMediaService mediaService )
        {
            _publishEndpoint = publishEndpoint;
            _mediaService = mediaService;
        }
        public async Task Handle(ImageUploadNotification request, CancellationToken cancellationToken)
        {            
            //handle upload
            var listUrlAfterUpload = await _mediaService.UploadImageToCloudinary(request.Data,request.CloudinaryFolder);
            //message
            var ImageUploadedNotification = new ImageUploadedNotification{
                Id = request.Id,
                Description = "Media was uploaded to cloud",
                Name = "Uploaded media",
                TimeStamp = DateTimeOffset.UtcNow,
                urlUpload = listUrlAfterUpload,
                Type = request.Type,
            };
            //send uploaded image to exchange topic
            using var source = new CancellationTokenSource(TimeSpan.FromSeconds(30));
            await _publishEndpoint.Publish(ImageUploadedNotification, source.Token);
        }
    }
}
