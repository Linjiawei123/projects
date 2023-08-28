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
        /// <param name="CustomerCode">供应商编号</param>
        /// <param name="CustomerName">供应商名称</param>
        /// <param name="TelephoneCode">联系电话</param>
        /// <param name="Email">邮箱</param>
        /// <param name="PostCode">邮政编码</param>
        /// <param name="Linkman">联系人</param>
        /// <param name="Url">网址</param>
        /// <param name="Address">地址</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<BSCustomer>>> GetPageAsync(string CustomerCode, string CustomerName, string TelephoneCode,
            string Email, string PostCode, string Linkman, string Url, string Address, short pageSize, int pageIndex);
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
