using Microsoft.EntityFrameworkCore;
using MicroService.Models;
using MicroService.Interfaces;

namespace MicroService.Repository
{
    internal class PermissionRepository : DbContext, IPermissionRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Permission> _permissionSet;
        public PermissionRepository(DataContext context)
        {
            _context = context;
            _permissionSet = _context.Set<Permission>();
        }

        /// <summary>
        /// 获取菜单权限
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns></returns>
        public async Task<List<Permission>> GetByMenuIdAsync(Guid menuId)
        {
            return await _permissionSet.AsNoTracking().Where(w => w.MenuId == menuId).ToListAsync();
        }

        /// <summary>
        /// 根据Id获取权限
        /// </summary>
        /// <param name="id">权限Id</param>
        /// <returns></returns>
        public async Task<Permission> GetByIdAsync(Guid id)
        {
            return await _permissionSet.AsNoTracking().Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Permission obj)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                _permissionSet.Add(obj);
                await _context.SaveChangesAsync();
                await tran.CommitAsync();
                return true;
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Permission obj)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                _context.Attach(obj);
                _context.Entry(obj).Property(q => q.MenuId).IsModified = true;
                _context.Entry(obj).Property(q => q.Name).IsModified = true;
                _context.Entry(obj).Property(q => q.Code).IsModified = true;
                _context.Entry(obj).Property(q => q.Remark).IsModified = true;
                _context.Entry(obj).Property(q => q.OperaterId).IsModified = true;
                _context.Entry(obj).Property(q => q.OperateTime).IsModified = true;
                await _context.SaveChangesAsync();
                await tran.CommitAsync();
                return true;
            }
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Permission obj)
        {
            _context.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 代码是否已存在
        /// </summary>
        /// <param name="code">权限代码</param>
        /// <returns></returns>
        public async Task<bool> IsHasByCode(string code)
        {
            return await _permissionSet.AsNoTracking().AnyAsync(w => w.Code == code);
        }
    }
}
