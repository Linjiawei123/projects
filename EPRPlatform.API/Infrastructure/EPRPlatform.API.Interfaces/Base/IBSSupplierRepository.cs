using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Interfaces
{
    public interface IBSSupplierRepository
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="SupplierCode">供应商编号</param>
        /// <param name="SupplierName">供应商名称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<BSSupplier>>> GetPageAsync(string SupplierCode, string SupplierName, short pageSize, int pageIndex);
        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<BSSupplier> GetAsync(long Id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(BSSupplier obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BSSupplier obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(BSSupplier obj);
        /// <summary>
        /// 判断存货编号是否存在
        /// </summary>
        /// <param name="SupplierCode">存货编号</param>
        /// <returns></returns>
        Task<bool> AnyAsync(string SupplierCode);
    }
}
