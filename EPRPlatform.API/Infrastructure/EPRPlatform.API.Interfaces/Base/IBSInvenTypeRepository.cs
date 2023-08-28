using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Interfaces
{
    public interface IBSInvenTypeRepository
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<BSInvenTypeSimple>> GetListAsync();
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<BSInvenType> GetAsync(int Id);
        /// <summary>
        /// 判断编号是否存在
        /// </summary>
        /// <param name="InvenTypeCode">编号</param>
        /// <returns></returns>
        Task<bool> AnyAsync(string InvenTypeCode);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(BSInvenType obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BSInvenType obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(BSInvenType obj);
    }
}
