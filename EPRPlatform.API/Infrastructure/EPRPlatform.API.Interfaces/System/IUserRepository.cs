using EPRPlatform.API.Models;

namespace EPRPlatform.API.Interfaces
{
    /// <summary>
    /// 用户信息接口
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <returns></returns>
        Task<User> GetByIdAsync(Guid Id);
        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="Account">账号</param>
        /// <returns></returns>
        Task<User> GetByAccountAsync(string Account);
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetListAsync();
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Name">昵称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<User>>> GetPageAsync(string Account, string Name, short pageSize, int pageIndex);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="addRights">用户权限</param>
        /// <returns></returns>
        Task<bool> AddAsync(User obj, List<UserAndPermission> addRights);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="delRights">新增权限</param>
        /// <param name="addRights">删除权限</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(User obj, List<UserAndPermission> addRights, List<UserAndPermission> delRights);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(User obj);
        /// <summary>
        /// 个人用户修改
        /// </summary>
        /// <param name="obj">用户信息</param>
        /// <returns></returns>
        Task<bool> UpdateUserInfoAsync(User obj);
    }
}
