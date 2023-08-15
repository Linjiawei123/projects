using MicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Interfaces
{
    /// <summary>
    /// 权限数据接口
    /// </summary>
    public interface IPermissionRepository
    {
        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns></returns>
        Task<List<Permission>> GetByMenuIdAsync(Guid menuId);
        /// <summary>
        /// 根据Id获取权限
        /// </summary>
        /// <param name="id">权限Id</param>
        /// <returns></returns>
        Task<Permission> GetByIdAsync(Guid id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(Permission obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Permission obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Permission obj);
        /// <summary>
        /// 代码是否已存在
        /// </summary>
        /// <param name="code">权限代码</param>
        /// <returns></returns>
        Task<bool> IsHasByCode(string code);
    }
}
