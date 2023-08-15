using MicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Interfaces
{
    public interface IChatMessageRepository
    {
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        Task<bool> AddMessageAsync(ChatMessage message);
        /// <summary>
        /// 获取聊天记录
        /// </summary>
        /// <param name="SenderId">发送者id</param>
        /// <param name="ReceiverId">接收者id</param>
        /// <param name="RecordSize">记录数</param>
        /// <returns></returns>
        Task<List<ChatMessage>> ChatRecordsAsync(string SenderId, string ReceiverId, int RecordSize);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <returns></returns>
        Task<List<User>> GetUsersAsync(Guid UserId);
        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="uIds">用户id</param>
        /// <param name="RecordSize">记录数</param>
        /// <returns></returns>
        Task<List<ChatMessage>> GetChatMessagesAsync(List<string> uIds, int RecordSize);
    }
}
