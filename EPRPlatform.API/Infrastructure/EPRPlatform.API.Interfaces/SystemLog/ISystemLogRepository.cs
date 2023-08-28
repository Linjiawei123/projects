using EPRPlatform.API.Models;

namespace EPRPlatform.API.Interfaces
{
    /// <summary>
    /// 日志
    /// </summary>
    public interface ISystemLogRepository
    {
        /// <summary>
        /// 获取用户登录日志
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="name">昵称</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<UserLoginLogSimple>>> GetLoginLogPageAsync(string account, string name,DateTime? beginTime,DateTime? endTime,short pageSize,int pageIndex);
        /// <summary>
        /// 获取用户操作日志
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="name">昵称</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<OperateLogSimple>>> GetOperateLogPageAsync(string account, string name, DateTime? beginTime, DateTime? endTime, short pageSize, int pageIndex);
        /// <summary>
        /// 添加用户操作日志
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddOperateLogAsync(OperateLog obj);
    }
}
