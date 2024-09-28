using capygram.Chat.Domain.Model;

namespace capygram.Chat.Domain.Repositories
{
    public interface IChatRepositories
    {
        Task<List<Messages>> GetAll();
        Task<Messages> DeleteMessageById( DeleteMessageRequest deleteMessageRequest );
        Task<List<Messages>> GetMessagesByUserId(Guid id);
        Task AddMessage (Messages message);
    }
}
