using Chat.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Chat.Repositories
{
    public class SentimentRepository : ISentimentRepository
    {
        private readonly SentimentChatDbContext _dbContext;

        public SentimentRepository(SentimentChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> GetIdByTitle(string title)
        {
            return (await _dbContext.Sentiments
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Title == title))
                .Id;
        }
    }
}
