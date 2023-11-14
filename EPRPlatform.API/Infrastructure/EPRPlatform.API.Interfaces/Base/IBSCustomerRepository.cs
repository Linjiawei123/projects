using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;

namespace EPRPlatform.API.Interfaces
{
    public interface IBSCustomerRepository
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="CustomerCode">客户编号</param>
        /// <param name="CustomerName">客户名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<BSCustomer>>> GetPageAsync(string CustomerCode, string CustomerName, short pageSize, int pageIndex);
        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<BSCustomer> GetAsync(long Id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(BSCustomer obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BSCustomer obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(BSCustomer obj);
        /// <summary>
        /// 判断存货编号是否存在
        /// </summary>
        /// <param name="CustomerCode">存货编号</param>
        /// <returns></returns>
        Task<bool> AnyAsync(string CustomerCode);
        /// <summary>
        /// 获取其他类型
        /// </summary>
        /// <returns></returns>
        Task<CUTypeSimple> GetOtherAsync();
    }
}
