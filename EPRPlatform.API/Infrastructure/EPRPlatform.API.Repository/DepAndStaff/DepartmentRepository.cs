using Microsoft.EntityFrameworkCore;
using EPRPlatform.API.Models;
using EPRPlatform.API.Method.EFCore;
using EPRPlatform.API.Interfaces;

namespace EPRPlatform.API.Repository
{
    public class DepartmentRepository : DbContext, IDepartmentRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Department> _departmentSet;
        public DepartmentRepository(DataContext context)
        {
            _context = context;
            _departmentSet = _context.Set<Department>();
        }
        /// <summary>
        /// 获取所有部门
        /// </summary>
        /// <returns></returns>
        public async Task<List<Department>> GetListAsync()
        {
            return await _departmentSet.AsNoTracking().NotDeleted().ToListAsync();
        }
        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id">Id</param>
        /// <returns></returns>
        public async Task<Department> GetByIdAsync(Guid id)
        {
            return await _departmentSet.AsNoTracking().NotDeleted().Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Department obj)
        {
            _departmentSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Department obj)
        {
            using var tran = _context.Database.BeginTransaction();
            _context.Attach(obj);
            _context.Entry(obj).Property(q => q.ParentId).IsModified = true;
            _context.Entry(obj).Property(q => q.Name).IsModified = true;
            _context.Entry(obj).Property(q => q.Code).IsModified = true;
            _context.Entry(obj).Property(q => q.OperaterId).IsModified = true;
            _context.Entry(obj).Property(q => q.OperateTime).IsModified = true;
            await _context.SaveChangesAsync();
            await tran.CommitAsync();
            return true;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Department obj)
        {
            _context.SoftDelete(obj);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 编号是否被使用
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        public async Task<bool> ExistsByCode(string code, Guid? id)
        {
            if (id != null)
                return await _departmentSet.NotDeleted().AnyAsync(q => q.Code == code && q.Id != id);
            else
                return await _departmentSet.NotDeleted().AnyAsync(q => q.Code == code);
        }
        /// <summary>
        /// 是否存在下级
        /// </summary>
        /// <param name="Id">菜单id</param>
        /// <returns></returns>
        public async Task<bool> HasChildAsync(Guid Id)
        {
            return await _departmentSet.NotDeleted().AnyAsync(q => q.ParentId == Id);
        }
    }
}
