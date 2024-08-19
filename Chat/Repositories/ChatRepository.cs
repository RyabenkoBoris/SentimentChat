using Chat.Interfaces;
using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly SentimentChatDbContext _dbContext;

        public ChatRepository(SentimentChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> GetIdByTitle(string title)
        {
            return (await _dbContext.Chats
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Title == title))
                .Id;
        }
        public async Task<bool> IsExist(string title)
        {
            var chatEntity = await _dbContext.Chats
                .FirstOrDefaultAsync(c => c.Title == title);

            return chatEntity is not null;
        }
        public async Task Add(string title)
        {
            if (await IsExist(title)) return;

            var chatEntity = new ChatEntity { Title = title };

            await _dbContext.AddAsync(chatEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
