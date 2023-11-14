using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Interfaces
{
    public interface IBSCostRepository
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="CostCode">费用编码</param>
        /// <param name="CostName">费用名称</param>
        /// <param name="CostTypeCode">分类</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<BSCostSimple>>> GetPageAsync(string CostCode, string CostName, string CostTypeCode, short pageSize, int pageIndex);
        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<BSCostSimple> GetAsync(long Id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(BSCost obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BSCost obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(BSCost obj);
        /// <summary>
        /// 判断费用编号是否存在
        /// </summary>
        /// <param name="CostCode">费用编号</param>
        /// <returns></returns>
        Task<bool> AnyAsync(string CostCode);
    }
}
