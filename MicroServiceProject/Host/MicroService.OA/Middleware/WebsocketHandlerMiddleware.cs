
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using MicroService.Method;
using MicroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.OA
{
    /// <summary>
    /// websocket中间件
    /// </summary>
    public class WebsocketHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        private readonly string RedisKey = "websocket_";
        private readonly IChatGroupRepository _iChatGroup;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="next"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="iChatGroup"></param>
        public WebsocketHandlerMiddleware(
            RequestDelegate next,
            ILoggerFactory loggerFactory, IChatGroupRepository iChatGroup
            )
        {
            _next = next;
            _logger = loggerFactory.
                CreateLogger<WebsocketHandlerMiddleware>();
            _iChatGroup = iChatGroup;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Path == "/ws")
            {
                if (context.WebSockets.IsWebSocketRequest)
                {
                    WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
                    if (int.TryParse(StringHepler.GetUrlParameterValue(context.Request.QueryString.Value, "UserId"), out int userId))
                    {
                        if (userId > 0)
                        {
                            WebsocketClient client = new() { UserId = userId, Websocket = webSocket };
                            try
                            {
                                await Handle(client);
                            }
                            catch
                            {
                                await context.Response.WriteAsync("closed");
                            }
                        }
                    }
                }
                else
                    context.Response.StatusCode = 404;
            }
            else
                await _next(context);
        }

        private async Task Handle(WebsocketClient client)
        {
            WebsocketClientCollection.Add(client);
            string key = RedisKey + client.UserId;
            List<Message> list = RedisHepler.GetList<Message>(key);
            if (list != null && list.Count > 0)
            {
                list = list.OrderBy(w => w.SendTime).ToList();
                list.ForEach(w => client.SendMessageAsync(StringHepler.ToJson<Message>(w)));
                RedisHepler.KeyDelete(key);
            }
            WebSocketReceiveResult result;
            do
            {
                var buffer = new byte[1024 * 50]; // 50M数据
                result = await client.Websocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Text && !result.CloseStatus.HasValue)
                {
                    var msgString = Encoding.UTF8.GetString(buffer);
                    string msg = msgString.Replace("/0", "").Trim();
                    var message = StringHepler.FromJson<Message>(msg);
                    message.FromUserId = client.UserId;
                    message.SendTime = DateTime.Now;
                    MessageRoute(message);
                }else if(result.MessageType == WebSocketMessageType.Binary && !result.CloseStatus.HasValue)
                {
                    
                }
            } while (!result.CloseStatus.HasValue); // 未关闭请求一直执行

        }

        private void MessageRoute(Message message)
        {
            var client = WebsocketClientCollection.Get(message.FromUserId);
            switch (message.Action)
            {
                case "send_to_one":
                    if (message.ToUserId != null)
                    {
                        var toClient = WebsocketClientCollection.Get(message.ToUserId.Value);
                        if (toClient == null)
                        {
                            string key = RedisKey + message.ToUserId;
                            List<Message> list = RedisHepler.GetList<Message>(key);
                            list.Add(message);
                            bool res = RedisHepler.AddList<Message>(key, list);
                        }
                        else
                            toClient.SendMessageAsync(StringHepler.ToJson<Message>(message));
                    }
                    break;
                case "send _to_group":
                    if (message.ToGroupId != null)
                    {
                        var ulist = _iChatGroup.GetChatGroupUsersAsync(message.ToGroupId.Value).Result;
                        if (ulist != null && ulist.Count > 0)
                        {
                            List<int> uids = ulist.Select(w => w.UserId).ToList();
                            var toClients = WebsocketClientCollection.GetGroupUser(uids);
                            toClients.ForEach(w => { w.SendMessageAsync(StringHepler.ToJson<Message>(message)); });
                        }
                    }
                    break;
                default:
                    break;
            }


        }
    }
}
