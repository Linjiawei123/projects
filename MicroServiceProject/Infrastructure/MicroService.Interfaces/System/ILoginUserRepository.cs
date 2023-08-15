using MicroService.Models;

namespace MicroService.Interfaces
{
    /// <summary>
    /// 用户登录信息接口
    /// </summary>
    public interface ILoginUserRepository
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        Task<User> GetUserAsync(Guid Id);
        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="Account">账号</param>
        /// <returns></returns>
        Task<User> GetByAccountAsync(string Account);
        /// <summary>
        /// 登录失败记录
        /// </summary>
        /// <param name="obj">用户实体</param>
        /// <returns></returns>
        Task<bool> LoginErrAsync(User obj);
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        Task<UserLogin> GetAsync(Guid UserId);
        /// <summary>
        /// 根据刷新token获取登录信息
        /// </summary>
        /// <param name="RefreshToken">刷新token</param>
        /// <returns></returns>
        Task<UserLogin> GetRefreshTokenAsync(string RefreshToken);
        /// <summary>
        /// 新增登录信息
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="log">日志</param>
        /// <returns></returns>
        Task<bool> AddAsync(UserLogin obj, UserLoginLog log);
        /// <summary>
        /// 修改登录信息
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="user">用户</param>
        /// <param name="log">日志</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(UserLogin obj, User user, UserLoginLog log);

        /// <summary>
        /// 根据token获取登录用户
        /// </summary>
        /// <param name="token">登录token</param>
        /// <returns></returns>
        Task<UserLogin> GetUserLoginAsync(string token);
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        Task<List<UserRights>> GetUserRightsAsync(Guid UserId);
        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        Task<List<Menu>> GetUserMenusAsync(Guid UserId);
    }
}
