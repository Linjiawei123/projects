using MicroService.Models;

namespace MicroService.Interfaces
{
    /// <summary>
    /// 角色数据接口
    /// </summary>
    public interface IRoleRepository
    {
        #region 角色基础数据
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="Keyword">关键词</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<Role>>> GetRolePageAsync(string Keyword, short pageSize, int pageIndex);
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<Role> Get(Guid Id);
        /// <summary>
        /// 编号是否已使用
        /// </summary>
        /// <param name="Code">编号</param>
        /// <returns></returns>
        Task<bool> AnyCode(string Code);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="rights">角色权限</param>
        /// <returns></returns>
        Task<bool> AddAsync(Role obj, List<RoleAndPermission> rights);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="addRights">新增的权限</param>
        /// <param name="delRights">删除的权限</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Role obj, List<RoleAndPermission> addRights, List<RoleAndPermission> delRights);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Role obj);
        #endregion
        #region 角色用户
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetUsersAsync();
        /// <summary>
        /// 获取角色用户列表
        /// </summary>
        /// <param name="RoleId">角色id</param>
        /// <returns></returns>
        Task<List<RoleUserInfo>> GetRoleAndUserAsync(Guid RoleId);
        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="RoleId">角色id</param>
        /// <returns></returns>
        Task<List<RoleAndUser>> GetRoleUsersAsync(Guid RoleId);
        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<RoleAndUser> GetRoleUserAsync(Guid Id);
        /// <summary>
        /// 角色成员
        /// </summary>
        /// <param name="addlist">新增</param>
        /// <param name="dellist">删除</param>
        /// <returns></returns>
        Task<bool> RoleUserAsync(List<RoleAndUser> addlist, List<RoleAndUser> dellist);
        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="Id">成员id</param>
        /// <returns></returns>
        Task<bool> DeleteRoleUserAsync(Guid Id);
        #endregion
        #region 角色权限
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <returns></returns>
        Task<List<RoleAndPermission>> GetRoleAndPermissionsAsync();
        #endregion
    }
}
