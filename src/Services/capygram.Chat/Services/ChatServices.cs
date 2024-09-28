using capygram.Chat.Domain.Model;
using capygram.Chat.Domain.Repositories;
using capygram.Common.Shared;

namespace capygram.Chat.Services
{
    public class ChatServices : IChatServices
    {
        private readonly IChatRepositories _repository;
        public ChatServices( IChatRepositories repositories ) { 
            _repository = repositories;
        }
        public async Task AddMessage(Messages message)
        {
            await _repository.AddMessage( message );            
        }

        public async Task<Messages> DeleteMessageById(DeleteMessageRequest deleteMessageRequest)
        {
            var message = await _repository.DeleteMessageById( deleteMessageRequest);
            return message;
        }

        public Task<List<Messages>> GetAllMessages()
        {
            throw new NotImplementedException();
        }

        public async Task<Result<List<Messages>>> GetMessagesByUserId(Guid userId)
        {
            var listMessages = await _repository.GetMessagesByUserId( userId );
            return Result<List<Messages>>.CreateResult(true, new ResultDetail { Code = "200", Message = "Messages" }, listMessages);
        }
    }
}
