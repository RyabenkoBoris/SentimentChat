namespace Chat.Interfaces
{
    public interface IUserRepository
    {
        Task Add(string name);
        Task<int> GetIdByName(string name);
        Task<bool> IsExist(string name);
    }
}