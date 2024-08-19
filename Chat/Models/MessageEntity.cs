namespace Chat.Models
{
    public class MessageEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public UserEntity? User { get; set; }
        public int ChatId { get; set; }
        public ChatEntity? Chat { get; set; }
        public int SentimentId { get; set; }
        public SentimentEntity? Sentiment { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
