using RealTimeConnect.DTOs;

namespace RealTimeConnect.Interfaces
{
    public interface IMessageService
    {
        Task SendMessageAsync(int senderId, int receiverId, string content);
        Task<List<MessageDto>> GetMessagesAsync(int userId1, int userId2);
    }
}
