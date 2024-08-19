using Chat.Interfaces;
using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly SentimentChatDbContext _dbContext;

        public UserRepository(SentimentChatDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> GetIdByName(string name)
        {
            return (await _dbContext.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(c => c.Name == name))
                .Id;
        }

        public async Task<bool> IsExist(string name)
        {
            var chatEntity = await _dbContext.Users
                .FirstOrDefaultAsync(c => c.Name == name);

            return chatEntity is not null;
        }
        public async Task Add(string name)
        {
            if (await IsExist(name)) return;

            var userEntity = new UserEntity { Name = name };

            await _dbContext.AddAsync(userEntity);
            await _dbContext.SaveChangesAsync();
        }
    }
}
