using Microsoft.EntityFrameworkCore;
using MicroService.Models;
using System.Linq.Expressions;
using MicroService.Method.EFCore;
using MicroService.Method;
using MicroService.Interfaces;

namespace MicroService.Repository
{
    public class UserRepository : DbContext, IUserRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<User> _userSet;
        private readonly DbSet<UserRights> _userRightsSet;
        private readonly DbSet<UserAndPermission> _userAndPermissionSet;
        public UserRepository(DataContext context)
        {
            _context = context;
            _userSet = _context.Set<User>();
            _userRightsSet = _context.Set<UserRights>();
            _userAndPermissionSet = _context.Set<UserAndPermission>();
        }

        /// <summary>
        /// 根据账号获取用户信息
        /// </summary>
        /// <param name="Account">账号</param>
        /// <returns></returns>
        public async Task<User> GetByAccountAsync(string Account)
        {
            return await _userSet.AsNoTracking().Where(w => w.Account == Account).FirstOrDefaultAsync();
        }
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="Id">用户id</param>
        /// <returns></returns>
        public async Task<User> GetByIdAsync(Guid Id)
        {
            return await _userSet.AsNoTracking().Where(w => w.Id == Id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetListAsync()
        {
            Expression<Func<User, bool>> exp = w => true;
            return await _userSet.AsNoTracking().Where(exp).ToListAsync();
        }

        public async Task<PageModel<List<User>>> GetPageAsync(string Account, string Name, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 10;
            IQueryable<User> query = GetQuery(Account, Name);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<User> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<User>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<User> GetQuery(string Account, string Name)
        {
            Expression<Func<User, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(Account))
                exp = exp.And(w => w.Account == Account);
            if (!string.IsNullOrEmpty(Name))
                exp = exp.And(w => w.Name.Contains(Name));
            return _userSet.AsNoTracking().Where(exp).Include(w => w.Rights).OrderBy(w => w.UserType).ThenBy(w => w.Id);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="rights">用户权限</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(User obj, List<UserAndPermission> rights)
        {
            try
            {
                using var tran = _context.Database.BeginTransaction();
                _userSet.Add(obj);
                if (rights.Count > 0)
                    _userAndPermissionSet.AddRange(rights);
                await _context.SaveChangesAsync();
                await tran.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="addRights">新增权限</param>
        /// <param name="delRights">删除权限</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(User obj, List<UserAndPermission> addRights, List<UserAndPermission> delRights)
        {
            try
            {
                using var tran = _context.Database.BeginTransaction();
                _context.Attach(obj);
                _context.Entry(obj).Property(q => q.Name).IsModified = true;
                _context.Entry(obj).Property(q => q.Password).IsModified = true;
                _context.Entry(obj).Property(q => q.Status).IsModified = true;
                _context.Entry(obj).Property(q => q.OperaterId).IsModified = true;
                _context.Entry(obj).Property(q => q.OperateTime).IsModified = true;
                if (addRights.Count > 0)
                    _userAndPermissionSet.AddRange(addRights);
                if (delRights.Count > 0)
                    _userAndPermissionSet.RemoveRange(delRights);
                await _context.SaveChangesAsync();
                await tran.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(User obj)
        {
            _context.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 个人用户修改
        /// </summary>
        /// <param name="obj">用户信息</param>
        /// <returns></returns>
        public async Task<bool> UpdateUserInfoAsync(User obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).Property(q => q.Name).IsModified = true;
            _context.Entry(obj).Property(q => q.Password).IsModified = true;
            _context.Entry(obj).Property(q => q.Url).IsModified = true;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
