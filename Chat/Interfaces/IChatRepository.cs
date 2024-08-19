namespace Chat.Interfaces
{
    public interface IChatRepository
    {
        Task Add(string title);
        Task<int> GetIdByTitle(string title);
        Task<bool> IsExist(string title);
    }
}