namespace Chat.Models
{
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<MessageEntity> Messages { get; set; } = [];
    }
}
