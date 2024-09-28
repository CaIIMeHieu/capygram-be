using capygram.Chat.Domain.Model;
using capygram.Common.Shared;

namespace capygram.Chat.Services
{
    public interface IChatServices
    {
        Task<List<Messages>> GetAllMessages();
        Task<Messages> DeleteMessageById(DeleteMessageRequest deleteMessageRequest);
        Task<Result<List<Messages>>> GetMessagesByUserId(Guid userId);
        Task AddMessage(Messages message);
    }
}
