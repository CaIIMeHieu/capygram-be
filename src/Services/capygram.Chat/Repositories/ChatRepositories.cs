using capygram.Chat.Domain.Data;
using capygram.Chat.Domain.Model;
using capygram.Chat.Domain.Repositories;
using Cassandra;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace capygram.Chat.Repositories
{
    public class ChatRepositories : IChatRepositories
    {
        public IChatContext _context { get; set; }
        public ChatRepositories(IChatContext context)
        {
            _context = context;
        }
        public async Task<Messages> DeleteMessageById(DeleteMessageRequest deleteMessageRequest)
        {
            try
            {
                var query = await _context.context.PrepareAsync("SELECT * FROM messages WHERE ID = ?;");
                var statement = query.Bind(deleteMessageRequest.Message.Id);
                var rows = await _context.context.ExecuteAsync(statement);
                var message = new Messages();
                foreach (var row in rows)
                {
                    message.Id = row.GetValue<Guid>("id");
                    message.Receiver = row.GetValue<Guid>("receiver");
                    message.Sender = row.GetValue<Guid>("sender");
                    message.Content = row.GetValue<string>("content");
                    message.CreatedAt = row.GetValue<DateTime>("created_at");
                    message.IsReceiverDeleted = row.GetValue<bool>("is_receiver_deleted");
                    message.IsSenderDeleted = row.GetValue<bool>("is_sender_deleted");
                }
                if (deleteMessageRequest.Type == DeleteType.deleteForEveryOne)
                {
                    message.IsSenderDeleted = true;
                    message.IsReceiverDeleted = true;
                }
                else
                {
                    message.IsSenderDeleted = deleteMessageRequest.UserRequest == message.Sender;
                    message.IsReceiverDeleted = deleteMessageRequest.UserRequest == message.Receiver;
                }
                // Cập nhật thông điệp vào cơ sở dữ liệu
                var updateQuery = await _context.context.PrepareAsync(
                    "UPDATE messages SET is_sender_deleted = ?, is_receiver_deleted = ? WHERE id = ? AND sender = ? AND receiver = ? AND created_at = ?;"
                );

                var updateStatement = updateQuery.Bind(
                    message.IsSenderDeleted,
                    message.IsReceiverDeleted,
                    message.Id,
                    message.Sender,
                    message.Receiver ,
                    message.CreatedAt
                );

                await _context.context.ExecuteAsync(updateStatement);

                return message;
            }
            catch (Exception ex) {
                throw new Exception();
            }
        }
         
        public Task<List<Messages>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<Messages>> GetMessagesByUserId(Guid id)
        {
            // Chuẩn bị truy vấn để lấy tất cả các tin nhắn mà người gửi có id tương ứng
            var querySender = await _context.context.PrepareAsync("SELECT * FROM messages WHERE sender = ?;");
            var statementSender = querySender.Bind(id);
            var rowsSender = await _context.context.ExecuteAsync(statementSender);

            // Chuẩn bị truy vấn để lấy tất cả các tin nhắn mà người nhận có id tương ứng
            var queryReceiver = await _context.context.PrepareAsync("SELECT * FROM messages WHERE receiver = ?;");
            var statementReceiver = queryReceiver.Bind(id);
            var rowsReceiver = await _context.context.ExecuteAsync(statementReceiver);

            // Kết hợp kết quả từ hai truy vấn
            var rows = rowsSender.Concat(rowsReceiver);

            var resutls = new List<Messages>();
            foreach (var row in rows) {
                var message = new Messages
                {
                    Id = row.GetValue<Guid>("id"),
                    Receiver = row.GetValue<Guid>("receiver"),
                    Sender = row.GetValue<Guid>("sender"),
                    Content = row.GetValue<string>("content"),
                    CreatedAt = row.GetValue<DateTime>("created_at"),
                    IsReceiverDeleted = row.GetValue<bool>("is_receiver_deleted"),
                    IsSenderDeleted = row.GetValue<bool>("is_sender_deleted"),
                };
                resutls.Add(message);
            }
            return resutls.OrderBy(m => m.CreatedAt).ToList(); ;
        }

        public async Task AddMessage(Messages message)
        {
            var query = @"INSERT INTO messages (id, sender, receiver, content, created_at, is_sender_deleted, is_receiver_deleted) 
                      VALUES (?, ?, ?, ?, ?, ?, ?)";

            var preparedStatement = await _context.context.PrepareAsync(query);

            var boundStatement = preparedStatement.Bind(
                message.Id,
                message.Sender,
                message.Receiver,
                message.Content,
                DateTime.Now,
                message.IsSenderDeleted,
                message.IsReceiverDeleted
            );

            await _context.context.ExecuteAsync(boundStatement);
        }
    }
}
