using MicroService.Extend;
using MicroService.Interfaces;
using MicroService.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace MicroService.SignalR
{
    public class ChatMessageTimeJob : BackgroundService
    {
        private readonly string key = "ChatMessage";
        private readonly IErrorRepository _iError;
        private readonly IRedisInvoker _iRedisInvoker;
        private readonly IChatMessageRepository _iChatMessageRepository;
        /// <summary>
        /// 构造函数
        /// </summary>
        public ChatMessageTimeJob(IErrorRepository iError, IRedisInvoker iRedisInvoker, IChatMessageRepository iChatMessageRepository)
        {
            try
            {
                _iError = iError;
                _iRedisInvoker = iRedisInvoker;
                _iChatMessageRepository = iChatMessageRepository;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 定时执行
        /// </summary>
        /// <param name="stoppingToken"></param>
        /// <returns></returns>
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            try
            {
                await new TaskFactory().StartNew(async () =>
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        try
                        {
                            ChatMessage message = await _iRedisInvoker.ListLeftPopAsync<ChatMessage>(key);
                            if (message != null)
                            {
                                try
                                {
                                    await _iChatMessageRepository.AddMessageAsync(message);
                                }
                                catch
                                {
                                    string json = JsonConvert.SerializeObject(message);
                                    await _iRedisInvoker.ListRightPushAsync(key, json);
                                }
                            }
                                
                        }
                        catch (Exception ex)
                        {
                            await _iError.AddErrorAsync(ex);
                        }
                        Thread.Sleep(5 * 1000 * 1);
                    }
                }, stoppingToken);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
            }
        }
    }
}
