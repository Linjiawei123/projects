using EPRPlatform.API.Models;

namespace EPRPlatform.API.Repository
{
    /// <summary>
    /// 职工
    /// </summary>
    public interface IEmployeeRepository
    {
        /// <summary>
        /// 获取职工分页数据
        /// </summary>
        /// <param name="departmentId">部门id</param>
        /// <param name="name">姓名</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        Task<PageModel<List<EmployeeInfo>>> GetPageAsync(Guid? departmentId,string name,short pageSize,int pageIndex);
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<EmployeeInfo> GetAsync(Guid Id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="employee">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(Employee employee);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="employee">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Employee employee);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="employee">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Employee employee);
        /// <summary>
        /// 编号是否已存在
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<bool> ExistsCodeAsync(string code,Guid? Id);
        /// <summary>
        /// 用户是否已被绑定
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        Task<bool> ExistsUserAsync(Guid UserId,Guid? Id);
        /// <summary>
        /// 获取未绑定职工的用户
        /// </summary>
        /// <returns></returns>
        Task<List<User>> GetNotBindUserAsync(Guid? Id);
    }
}
