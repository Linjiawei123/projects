using MicroService.Models;

namespace MicroService.Interfaces
{
    /// <summary>
    /// 部门
    /// </summary>
    public  interface IDepartmentRepository
    {
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        Task<List<Department>> GetListAsync();
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Department> GetByIdAsync(Guid id);
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> AddAsync(Department obj);
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> UpdateAsync(Department obj);
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        Task<bool> DeleteAsync(Department obj);
        /// <summary>
        /// 编号是否被使用
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        Task<bool> ExistsByCode(string code, Guid? id);
        /// <summary>
        /// 是否存在下级
        /// </summary>
        /// <param name="Id">菜单id</param>
        /// <returns></returns>
        Task<bool> HasChildAsync(Guid Id);
    }
}
