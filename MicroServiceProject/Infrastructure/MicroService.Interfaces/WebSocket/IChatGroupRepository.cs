using MicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Interfaces
{
    /// <summary>
    /// 群聊组
    /// </summary>
    public interface IChatGroupRepository
    {
        /// <summary>
        /// 获取群聊组成员
        /// </summary>
        /// <param name="GroupId">组id</param>
        /// <returns></returns>
        Task<List<ChatGroupUser>> GetChatGroupUsersAsync(int GroupId);
    }
}
