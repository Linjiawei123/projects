using Microsoft.EntityFrameworkCore;
using MicroService.Models;
using MicroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Repository
{
    internal class ChatGroupRepository:DbContext, IChatGroupRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<ChatGroup> _chatGroupSet;
        private readonly DbSet<ChatGroupUser> _chatGroupUserSet;
        public ChatGroupRepository(DataContext context)
        {
            _context = context;
            _chatGroupSet = _context.Set<ChatGroup>();
            _chatGroupUserSet = _context.Set<ChatGroupUser>();
        }
        /// <summary>
        /// 获取群聊组成员
        /// </summary>
        /// <param name="GroupId">组id</param>
        /// <returns></returns>
        public async Task<List<ChatGroupUser>> GetChatGroupUsersAsync(int GroupId)
        {
            return await _chatGroupUserSet.AsNoTracking().Where(w => w.GroupId == GroupId).OrderBy(w => w.AddTime).ToListAsync();
        }
    }
}
