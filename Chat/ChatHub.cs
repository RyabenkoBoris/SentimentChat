using Chat.Interfaces;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Chat
{
    public interface IChatClient
    {
        public Task ReceiveMessage(string userName, string message);
    }

    public class ChatHub : Hub<IChatClient>
    {
        private readonly IChatRepository _chatRepository;
        private readonly IMessageRepository _messageRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISentimentRepository _sentimentRepository;
        private readonly IDistributedCache _cache;

        public ChatHub(IDistributedCache cache, 
            IChatRepository chatRepository, 
            IMessageRepository messageRepository, 
            IUserRepository userRepository,
            ISentimentRepository sentimentRepository)
        {
            _cache = cache;
            _chatRepository = chatRepository;
            _messageRepository = messageRepository;
            _userRepository = userRepository;
            _sentimentRepository = sentimentRepository;
        }

        public async Task JoinChat(UserConnection connection)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, connection.ChatRoom);

            var stringConnection = JsonSerializer.Serialize(connection);
            await _cache.SetStringAsync(Context.ConnectionId, stringConnection);
            
            var messages = await _messageRepository.GetChatMessages(await _chatRepository.GetIdByTitle(connection.ChatRoom));
            
            foreach(var message in messages)
            {
                await Clients.Caller.ReceiveMessage(message.User.Name, $"{message.Sentiment?.Title}#{message.Message}");
            }
            
            await Clients
                .Group(connection.ChatRoom)
                .ReceiveMessage(string.Empty, $"#{connection.UserName} was connected");

            await _chatRepository.Add(connection.ChatRoom);
            await _userRepository.Add(connection.UserName);
        }

        public async Task SendMessage(string message)
        {
            var stringConnection = await _cache.GetAsync(Context.ConnectionId);

            var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

            if (connection is not null)
            {
                var sentimentAnalys = await SentimentAnalysis.SentimentAnalysMessage(message);
                
                await _messageRepository.Add(
                    await _userRepository.GetIdByName(connection.UserName),
                    await _chatRepository.GetIdByTitle(connection.ChatRoom), 
                    await _sentimentRepository.GetIdByTitle(sentimentAnalys), 
                    message);
                
                await Clients
                    .Group(connection.ChatRoom)
                    .ReceiveMessage(connection.UserName, $"{sentimentAnalys}#" + message);
            }
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var stringConnection = await _cache.GetAsync(Context.ConnectionId);
            var connection = JsonSerializer.Deserialize<UserConnection>(stringConnection);

            if (connection is not null)
            {
                await _cache.RemoveAsync(Context.ConnectionId);
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, connection.ChatRoom);

                await Clients
                    .Group(connection.ChatRoom)
                    .ReceiveMessage(string.Empty, $"#{connection.UserName} was disconnected");
            }

            await base.OnDisconnectedAsync(exception);
        }
    }
}
