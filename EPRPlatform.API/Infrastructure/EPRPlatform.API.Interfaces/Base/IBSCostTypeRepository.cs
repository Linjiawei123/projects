using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Models.Base;

namespace EPRPlatform.API.Interfaces
{
    public interface IBSCostTypeRepository
    {
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        Task<List<BSCostTypeSimple>> GetListAsync();
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<BSCostType> GetAsync(int Id);
        /// <summary>
        /// 判断编号是否存在
        /// </summary>
        /// <param name="CostTypeCode">编号</param>
        /// <returns></returns>
        Task<bool> AnyAsync(string CostTypeCode);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(BSCostType obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BSCostType obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(BSCostType obj);
    }
}
