namespace Chat.Interfaces
{
    public interface ISentimentRepository
    {
        Task<int> GetIdByTitle(string title);
    }
}