﻿using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Interfaces
{
    public interface IBSInvenRepository
    {
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="InvenCode">存货编码</param>
        /// <param name="InvenName">存货名称</param>
        /// <param name="InvenTypeCode">存货类别</param>
        /// <param name="SpecsModel">规格型号</param>
        /// <param name="MeaUnit">计量单位</param>
        /// <param name="SelPrice">参考售价</param>
        /// <param name="PurPrice">参考进价</param>
        /// <param name="SmallStockNum">最低库存</param>
        /// <param name="BigStockNum">最高库存</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<BSInvenSimple>>> GetPageAsync(string InvenCode, string InvenName, string InvenTypeCode, string SpecsModel, string MeaUnit, decimal? SelPrice, decimal? PurPrice, int? SmallStockNum, int? BigStockNum, short pageSize, int pageIndex);
        /// <summary>
        /// 根据id获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<BSInvenSimple> GetAsync(long Id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(BSInven obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(BSInven obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(BSInven obj);
        /// <summary>
        /// 判断存货编号是否存在
        /// </summary>
        /// <param name="InvenCode">存货编号</param>
        /// <returns></returns>
        Task<bool> AnyAsync(string InvenCode);
    }
}
