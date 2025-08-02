using Microsoft.EntityFrameworkCore;
using RealTimeConnect.Database;
using RealTimeConnect.DTOs;
using RealTimeConnect.Interfaces;
using RealTimeConnect.Models;

namespace RealTimeConnect.Implementations
{
    public class MessageService : IMessageService
    {
        private readonly AppDbContext context;

        public MessageService(AppDbContext context)
        {
            this.context = context;
        }

        public async Task SendMessageAsync(int senderId, int receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                CreatedAt = DateTime.UtcNow
            };

            context.Messages.Add(message);
            await context.SaveChangesAsync();
        }


        public async Task<List<MessageDto>> GetMessagesAsync(int userId1, int userId2)
        {
            var messages = await context.Messages.Include(u => u.Sender)
                .Where(m =>
                    (m.SenderId == userId1 && m.ReceiverId == userId2) ||
                    (m.SenderId == userId2 && m.ReceiverId == userId1))
                .OrderBy(m => m.CreatedAt)
                .Select(m => new MessageDto
                {
                    Id = m.Id,
                    Content = m.Content,
                    SenderId = m.SenderId,
                    SenderName = m.Sender.UserName,
                    ReceiverId = m.ReceiverId,
                    CreatedAt = m.CreatedAt
                })
                .ToListAsync();

            return messages;
        }
    }
}
