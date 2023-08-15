using IdentityServer4.Models;
using MicroService.Interfaces;
using MicroService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Repository
{
    public class ChatMessageRepository : DataContext, IChatMessageRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<User> _userSet;
        private readonly DbSet<ChatMessage> _chatMessageSet;
        public ChatMessageRepository(DataContext context)
        {
            _context = context;
            _userSet = _context.Set<User>();
            _chatMessageSet = _context.Set<ChatMessage>();
        }
        /// <summary>
        /// 新增信息
        /// </summary>
        /// <param name="message">信息内容</param>
        /// <returns></returns>
        public async Task<bool> AddMessageAsync(ChatMessage message)
        {
            _chatMessageSet.Add(message);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 获取聊天记录
        /// </summary>
        /// <param name="SenderId">发送者id</param>
        /// <param name="ReceiverId">接收者id</param>
        /// <param name="RecordSize">记录数</param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> ChatRecordsAsync(string SenderId, string ReceiverId, int RecordSize)
        {
            return await _chatMessageSet.AsNoTracking().Where(w => w.Equals(SenderId) && w.Equals(ReceiverId)).OrderByDescending(w => w.SendTime).Take(RecordSize).ToListAsync();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync(Guid UserId)
        {
            return await _userSet.AsNoTracking().Where(w => w.Id != UserId).ToListAsync();
        }


        /// <summary>
        /// 获取用户信息列表
        /// </summary>
        /// <param name="uIds">用户id</param>
        /// <param name="RecordSize">记录数</param>
        /// <returns></returns>
        public async Task<List<ChatMessage>> GetChatMessagesAsync(List<string> uIds, int RecordSize)
        {
            var query = _chatMessageSet
                .FromSqlRaw(@"SELECT [Id], [SenderId], [ReceiverId], [Content],[Type], [SendTime] 
                                    FROM ( SELECT [Id], [SenderId], [ReceiverId], [Content],[Type], [SendTime], ROW_NUMBER() OVER (PARTITION BY [SenderId] ORDER BY [SendTime] DESC) AS RowNum
                                    FROM [OfficePlatform].[dbo].[TbChatMessage]
                            ) AS Subquery
                            WHERE RowNum <= {0}", RecordSize)
                .Where(msg => uIds.Contains(msg.SenderId) || uIds.Contains(msg.ReceiverId))
                .OrderBy(msg => msg.SenderId)
                .ThenByDescending(msg => msg.SendTime);
            return await query.ToListAsync();
        }

    }
}
