using Chat.Configurations;
using Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Chat
{
    public class SentimentChatDbContext(DbContextOptions<SentimentChatDbContext> options) 
        : DbContext(options)
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ChatEntity> Chats { get; set; }
        public DbSet<SentimentEntity> Sentiments { get; set; }
        public DbSet<MessageEntity> Messages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new ChatConfiguration());
            modelBuilder.ApplyConfiguration(new SentimentConfiguration());
            modelBuilder.ApplyConfiguration(new MessageConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
