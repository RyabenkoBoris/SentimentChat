namespace Chat.Models
{
    public class ChatEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public List<MessageEntity> Messages { get; set; } = [];
    }
}
