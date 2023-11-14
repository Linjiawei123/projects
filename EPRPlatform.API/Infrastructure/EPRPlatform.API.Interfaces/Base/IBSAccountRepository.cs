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
    public interface IBSAccountRepository
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="AccountCode">存货编码</param>
        /// <param name="AccountName">存货名称</param>
        /// <param name="AccountTypeCode">分类</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<BSAccount>>> GetPageAsync(string AccountCode, string AccountName, short pageSize, int pageIndex);
        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<BSAccount> GetAsync(long Id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(BSAccount obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BSAccount obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(BSAccount obj);
        /// <summary>
        /// 判断存货编号是否存在
        /// </summary>
        /// <param name="AccountCode">存货编号</param>
        /// <returns></returns>
        Task<bool> AnyAsync(string AccountCode);
    }
}
