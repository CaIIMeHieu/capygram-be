using capygram.Auth.Domain.Repositories;
using capygram.Auth.Services;
using capygram.Common.DTOs.User;
using capygram.Common.Exceptions;
using capygram.Common.MessageBus.Events;
using MediatR;

namespace capygram.Auth.UseCases.Events
{
    public class UserMediaNotificationConsumerHandler : IRequestHandler<ImageUploadedNotification>
    {
        private readonly IUserServices _userServices;
        private readonly IUserRepository _userRepository;
        public UserMediaNotificationConsumerHandler( IUserServices userServices , IUserRepository userRepository )
        {
            _userServices = userServices;
            _userRepository = userRepository;
        }
        public async Task Handle( ImageUploadedNotification request, CancellationToken cancellationToken)
        {
            var infomation = new UserUpdateProfileDto();
            var user = await _userRepository.GetUserByIdAsync(request.Id);
            if (user is null)
            {
                throw new NotFoundException("User Is Not Found");
            }
            var userProfile = new UserUpdateProfileDto
            {
                Id = request.Id,
                Story = user.Profile.Story,
                AvatarUrl = request.urlUpload.First(),
                Gender = user.Profile.Gender
            };
            await _userServices.UpdateProfile(userProfile);
        }
    }
}
