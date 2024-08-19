namespace Chat.Models
{
    public class SentimentEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<MessageEntity> Messages { get; set; } = [];
    }
}
