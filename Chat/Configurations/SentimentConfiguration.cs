using Chat.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.Configurations
{
    public class SentimentConfiguration : IEntityTypeConfiguration<SentimentEntity>
    {
        public void Configure(EntityTypeBuilder<SentimentEntity> builder)
        {
            builder.HasKey(s => s.Id);

            builder
                .HasMany(s => s.Messages)
                .WithOne(m => m.Sentiment);

            builder.HasData(
                new SentimentEntity { Id = 1, Title = "Positive" },
                new SentimentEntity { Id = 2, Title = "Negative" },
                new SentimentEntity { Id = 3, Title = "Neutral" });
        }
    }
}
