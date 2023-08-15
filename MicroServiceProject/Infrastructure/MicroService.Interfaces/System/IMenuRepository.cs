using MicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Interfaces
{
    /// <summary>
    /// 菜单数据接口
    /// </summary>
    public interface IMenuRepository
    {
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        Task<List<Menu>> GetListAsync();
        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        Task<List<MenuRights>> GetMenuRightsAsync();
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Menu> GetByIdAsync(Guid id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(Menu obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Menu obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Menu obj);
        /// <summary>
        /// 编号是否被使用
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        Task<bool> ExistsByCode(string code,Guid? id);
        /// <summary>
        /// 是否存在下级
        /// </summary>
        /// <param name="Id">菜单id</param>
        /// <returns></returns>
        Task<bool> HasChildAsync(Guid Id);
    }
}
