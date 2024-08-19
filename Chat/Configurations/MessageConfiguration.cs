using Chat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Configurations
{
    public class MessageConfiguration : IEntityTypeConfiguration<MessageEntity>
    {
        public void Configure(EntityTypeBuilder<MessageEntity> builder)
        {
            builder.HasKey(m => m.Id);

            builder
                .HasOne(m => m.User)
                .WithMany(u => u.Messages);

            builder
                .HasOne(m => m.Chat)
                .WithMany(c => c.Messages);

            builder
                .HasOne(m => m.Sentiment)
                .WithMany(s => s.Messages);
        }
    }
}
