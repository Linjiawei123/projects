using MicroService.Extend;
using MicroService.Interfaces;
using MicroService.Models;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace MicroService.SignalR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly string key = "ChatMessage";
        private readonly IErrorRepository _iError;
        private readonly IRedisInvoker _iRedisInvoker;
        private static Dictionary<string, string> userConnections = new Dictionary<string, string>();
        public ChatHub(IRedisInvoker iRedisInvoker, IErrorRepository iError)
        {
            _iRedisInvoker = iRedisInvoker;
            _iError = iError;
        }

        public override async Task OnConnectedAsync()
        {
            string userId = Context.GetHttpContext().Request.Query["userId"];

            if (!string.IsNullOrEmpty(userId))
            {
                if (!userConnections.ContainsKey(userId))
                {
                    userConnections.Add(userId, Context.ConnectionId);
                }
                else
                {
                    userConnections[userId] = Context.ConnectionId;
                }
            }
            UpdateConnectedUsers();
            await base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            if (userConnections.ContainsValue(Context.ConnectionId))
                userConnections.Remove(userConnections.FirstOrDefault(w => w.Value == Context.ConnectionId).Key);
            UpdateConnectedUsers();
            return base.OnDisconnectedAsync(exception);
        }

        public void UpdateConnectedUsers()
        {
            Clients.All.SendAsync("ConnectedUsers", userConnections.Select(w => w.Key).ToList());
        }

        public async Task<ChatMessage> SendMessageToUser(string receiverUserId, string message)
        {
            try
            {
                string senderConnectionId = Context.ConnectionId;
                var senderUserId = userConnections.FirstOrDefault(x => x.Value == senderConnectionId).Key;
                if (userConnections.TryGetValue(receiverUserId, out string connectionId))
                {
                    ChatMessage chatMessage = new()
                    {
                        Id = Guid.NewGuid(),
                        SenderId = senderUserId,
                        ReceiverId = receiverUserId,
                        Content = message,
                        Type = 1,
                        SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", chatMessage, receiverUserId);
                    string json = JsonConvert.SerializeObject(chatMessage);
                    _iRedisInvoker.ListRightPush(key, json);
                    return chatMessage;
                }
                else
                {
                    ChatMessage chatMessage = new()
                    {
                        Id = Guid.NewGuid(),
                        SenderId = senderUserId,
                        ReceiverId = receiverUserId,
                        Content = message,
                        Type = 1,
                        SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    string json = JsonConvert.SerializeObject(chatMessage);
                    _iRedisInvoker.ListRightPush(key, json);
                    return chatMessage;
                }
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return null;
            }

        }

        public async Task<ChatMessage> SendEmoji(string receiverUserId, string emoji)
        {
            try
            {
                string senderConnectionId = Context.ConnectionId;
                var senderUserId = userConnections.FirstOrDefault(x => x.Value == senderConnectionId).Key;
                if (userConnections.TryGetValue(receiverUserId, out string connectionId))
                {
                    ChatMessage chatMessage = new()
                    {
                        Id = Guid.NewGuid(),
                        SenderId = senderUserId,
                        ReceiverId = receiverUserId,
                        Content = emoji,
                        Type = 2,
                        SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", chatMessage, receiverUserId);
                    string json = JsonConvert.SerializeObject(chatMessage);
                    _iRedisInvoker.ListRightPush(key, json);
                    return chatMessage;
                }
                else
                {
                    ChatMessage chatMessage = new()
                    {
                        Id = Guid.NewGuid(),
                        SenderId = senderUserId,
                        ReceiverId = receiverUserId,
                        Content = emoji,
                        Type = 2,
                        SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    string json = JsonConvert.SerializeObject(chatMessage);
                    _iRedisInvoker.ListRightPush(key, json);
                    return chatMessage;
                }
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return null;
            }
        }

        public async Task<ChatMessage> SendImage(string receiverUserId, string imageBase64)
        {
            try
            {
                string senderConnectionId = Context.ConnectionId;
                var senderUserId = userConnections.FirstOrDefault(x => x.Value == senderConnectionId).Key;
                if (userConnections.TryGetValue(receiverUserId, out string connectionId))
                {
                    ChatMessage chatMessage = new()
                    {
                        Id = Guid.NewGuid(),
                        SenderId = senderUserId,
                        ReceiverId = receiverUserId,
                        Content = imageBase64,
                        Type = 3,
                        SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    await Clients.Client(connectionId).SendAsync("ReceiveMessage", chatMessage, receiverUserId);
                    string json = JsonConvert.SerializeObject(chatMessage);
                    _iRedisInvoker.ListRightPush(key, json);
                    return chatMessage;
                }
                else
                {
                    ChatMessage chatMessage = new()
                    {
                        Id = Guid.NewGuid(),
                        SenderId = senderUserId,
                        ReceiverId = receiverUserId,
                        Content = imageBase64,
                        Type = 3,
                        SendTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")
                    };
                    string json = JsonConvert.SerializeObject(chatMessage);
                    _iRedisInvoker.ListRightPush(key, json);
                    return chatMessage;
                }
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return null;
            }
        }
    }
}
