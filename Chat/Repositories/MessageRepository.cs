using Chat.Interfaces;
using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly SentimentChatDbContext _dbContext;

        public MessageRepository(SentimentChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<MessageEntity>> GetChatMessages(int chatId)
        {
            return await _dbContext.Messages
                .AsNoTracking()
                .Include(m => m.User)
                .Include(m => m.Sentiment)
                .Where(m => m.ChatId == chatId)
                .ToListAsync();
        }

        public async Task Add(int userId, int chatId, int sentimentId, string message)
        {
            var messageEntity = new MessageEntity
            {
                UserId = userId,
                ChatId = chatId,
                SentimentId = sentimentId,
                Message = message
            };

            await _dbContext.AddAsync(messageEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
