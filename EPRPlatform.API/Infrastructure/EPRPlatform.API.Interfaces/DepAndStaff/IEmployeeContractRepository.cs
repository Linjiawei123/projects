using EPRPlatform.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Interfaces
{
    /// <summary>
    /// 员工合同
    /// </summary>
    public interface IEmployeeContractRepository
    {
        /// <summary>
        /// 获取合同分页
        /// </summary>
        /// <param name="name">职工姓名</param>
        /// <param name="code">合同编号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<EmployeeContractSimple>>> GetPageAsync(string name,string code,short pageSize,int pageIndex);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">合同id</param>
        /// <returns></returns>
        Task<EmployeeContractSimple> GetAsync(Guid id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(EmployeeContract obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(EmployeeContract obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(EmployeeContract obj);
    }
}
