using capygram.Chat.Domain.Model;
using capygram.Chat.Services;
using MassTransit.Testing;
using Microsoft.AspNetCore.SignalR;

namespace capygram.Chat.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatServices _chatServices;
        public ChatHub( IChatServices chatServices ) {
            _chatServices = chatServices;
        }
        public class UserConnection
        {
            public string ConnectionId { get; set; }
            public string UserName { get; set; }
            public Guid UserId { get; set; }
        }
        static List<UserConnection> Users = new List<UserConnection>();
        public async Task SendMessageToUser( Messages messages )
        {
            var receiver = Users.FirstOrDefault( item => item.UserId == messages.Receiver );
            var connectionId = receiver == null ? "offlineUser" : receiver.ConnectionId;
            await _chatServices.AddMessage( messages );
            messages.CreatedAt = DateTime.UtcNow;
            await Clients.Client(connectionId).SendAsync("ReceiveDM", messages);
        }
        public async Task DeleteMessageToUser(DeleteMessageRequest deleteMessageRequest)
        {
            var deletedMessage = await _chatServices.DeleteMessageById(deleteMessageRequest);
            await Clients.All.SendAsync("BroadCastDeleteMessage",deletedMessage);
        }
        public async Task PublishUserOnConnect( Guid UserId , string UserName )
        {
            var connection = new UserConnection
                {
                    ConnectionId = Context.ConnectionId,
                    UserId = UserId ,
                    UserName = UserName,
                };
            var index = Users.FindIndex(item => item.UserId == UserId);
            if ( index != -1 )
            {
                Users[index] = connection;
            }    
            else
            {
                Users.Add(connection);
            }
            await Clients.All.SendAsync("BroadcastUserOnConnect", Users);
        }

        public async Task RemoveOnlineUser(Guid userID)
        {
            var index = Users.FindIndex(item => item.UserId == userID);
            Users.RemoveAt(index);
            await Clients.All.SendAsync("BroadcastUserOnDisconnect",Users);
        }

    }
}
