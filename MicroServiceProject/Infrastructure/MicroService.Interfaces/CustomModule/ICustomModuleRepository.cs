using MicroService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Interfaces
{
    /// <summary>
    /// 自定义模块
    /// </summary>
    public interface ICustomModuleRepository
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<Module>> GetListAsync();
        /// <summary>
        /// 根基id获取
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        Task<Module> GetByIdAsync(int id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(Module obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Module obj);
        /// <summary>
        /// 属性详情
        /// </summary>
        /// <param name="id">属性id</param>
        /// <returns></returns>
        Task<Property> GetPropertyAsync(int id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddPropertyAsync(Property obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdatePropertyAsync(Property obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeletePropertyAsync(Property obj);
    }
}
