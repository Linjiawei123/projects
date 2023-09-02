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
    public interface IBSStoreRepository
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="StoreCode">存货编码</param>
        /// <param name="StoreName">存货名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<BSStoreSimple>>> GetPageAsync(string StoreCode, string StoreName,short pageSize, int pageIndex);
        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<BSStoreSimple> GetAsync(long Id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(BSStore obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BSStore obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(BSStore obj);
        /// <summary>
        /// 判断存货编号是否存在
        /// </summary>
        /// <param name="StoreCode">存货编号</param>
        /// <returns></returns>
        Task<bool> AnyAsync(string StoreCode);
        /// <summary>
        /// 获取员工列表
        /// </summary>
        /// <returns></returns>
        Task<List<BSEmployee>> GetEmployeesAsync();
    }
}
