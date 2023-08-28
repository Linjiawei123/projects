using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;

namespace EPRPlatform.API.Interfaces
{
    public interface IBSEmployeeRepository
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="EmployeeCode">供应商编号</param>
        /// <param name="EmployeeName">供应商名称</param>
        /// <param name="TelephoneCode">联系电话</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<BSEmployeeSimple>>> GetPageAsync(string EmployeeCode, string EmployeeName, string TelephoneCode, short pageSize, int pageIndex);
        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<BSEmployeeSimple> GetAsync(long Id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(BSEmployee obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BSEmployee obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(BSEmployee obj);
        /// <summary>
        /// 判断存货编号是否存在
        /// </summary>
        /// <param name="EmployeeCode">存货编号</param>
        /// <returns></returns>
        Task<bool> AnyAsync(string EmployeeCode);
        Task<INSimple> GetOtherAsync();
    }
}
