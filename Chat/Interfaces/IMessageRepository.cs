using Chat.Models;

namespace Chat.Interfaces
{
    public interface IMessageRepository
    {
        Task Add(int userId, int chatId, int sentimentId, string message);
        Task<List<MessageEntity>> GetChatMessages(int chatId);
    }
}