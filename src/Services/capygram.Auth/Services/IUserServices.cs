using capygram.Auth.Domain.Model;
using capygram.Common.DTOs.User;
using capygram.Common.Shared;

namespace capygram.Auth.Services
{
    public interface IUserServices
    {
        Task<Result<UserAuthenticationResponse>> Login(UserAuthenticationDto request);
        Task<Result<string>> Register(UserRegisterDto request);
        Task<Result<UserAuthenticationResponse>> ActiveAccount(UserRegisterDto request);
        Task<Result<string>> Logout(Guid Id);
        Task<Result<UserAuthenticationResponse>> RefreshToken(UserRefreshTokenDto request);
        Task<Result<string>> UpdateProfile(UserUpdateProfileDto request);
        Task<Result<string>> UploadAvatar(List<IFormFile> fileToUpload, Guid userId);
        Task<Result<User>> GetUserByUserID(Guid UserID);
        Task<Result<User?>> GetUserByName(string UserName);
    }
}
