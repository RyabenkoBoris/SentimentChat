using Chat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Configurations
{
    public class ChatConfiguration : IEntityTypeConfiguration<ChatEntity>
    {
        public void Configure(EntityTypeBuilder<ChatEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder
                .HasMany(c => c.Messages)
                .WithOne(m => m.Chat);
        }
    }
}
