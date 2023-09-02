using Microsoft.EntityFrameworkCore;
using EPRPlatform.API.Models;
using EPRPlatform.API.Method;
using EPRPlatform.API.Method.EFCore;
using EPRPlatform.API.Interfaces;
using System.Linq.Expressions;

namespace EPRPlatform.API.Repository
{
    public class RoleRepository : DbContext, IRoleRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<User> _userSet;
        private readonly DbSet<Role> _roleSet;
        private readonly DbSet<RoleAndUser> _roleAndUserSet;
        private readonly DbSet<RoleAndPermission> _roleAndPermissionSet;
        public RoleRepository(DataContext context)
        {
            _context = context;
            _userSet = _context.Set<User>();
            _roleSet = _context.Set<Role>();
            _roleAndUserSet = _context.Set<RoleAndUser>();
            _roleAndPermissionSet = _context.Set<RoleAndPermission>();
        }
        #region 角色基础数据
        /// <summary>
        /// 获取角色列表
        /// </summary>
        /// <param name="Keyword">关键词</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<PageModel<List<Role>>> GetRolePageAsync(string Keyword, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 10;
            IQueryable<Role> query = GetRoleQuery(Keyword);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<Role> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<Role>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };

        }

        private IQueryable<Role> GetRoleQuery(string Keyword)
        {
            Expression<Func<Role, bool>> exp = w => true;
            if (!string.IsNullOrWhiteSpace(Keyword))
                exp = exp.And(w => w.Name.Contains(Keyword));
            return _roleSet.AsNoTracking().Where(exp).Include(w => w.Rights).OrderBy(w => w.Code).ThenBy(w => w.Status == true).ThenBy(w => w.Id);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        public async Task<Role> Get(Guid Id)
        {
            return await _roleSet.AsNoTracking().Where(w => w.Id == Id).Include(w => w.Rights).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 编号是否已使用
        /// </summary>
        /// <param name="Code">编号</param>
        /// <returns></returns>
        public async Task<bool> AnyCode(string Code)
        {
            return await _roleSet.AsNoTracking().AnyAsync(w => w.Code == Code);
        }


        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="rights">角色权限</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Role obj, List<RoleAndPermission> rights)
        {
            try
            {
                using var tran = _context.Database.BeginTransaction();
                _roleSet.Add(obj);
                await _context.SaveChangesAsync();
                if (rights.Count > 0)
                {
                    _roleAndPermissionSet.AddRange(rights);
                    await _context.SaveChangesAsync();
                }
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
        /// <param name="obj"></param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Role obj)
        {
            _roleSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="addRights">新增的权限</param>
        /// <param name="delRights">删除的权限</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Role obj, List<RoleAndPermission> addRights, List<RoleAndPermission> delRights)
        {
            try
            {
                using var tran = _context.Database.BeginTransaction();
                _context.Attach(obj);
                _context.Entry(obj).State = EntityState.Modified;//修改全部
                await _context.SaveChangesAsync();
                if (addRights.Count > 0)
                    _roleAndPermissionSet.AddRange(addRights);
                if (delRights.Count > 0)
                    _roleAndPermissionSet.RemoveRange(delRights);
                await _context.SaveChangesAsync();
                await tran.CommitAsync();
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
        #endregion
        #region 角色用户
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetUsersAsync()
        {
            return await _context.Set<User>().AsNoTracking().ToListAsync();
        }
        /// <summary>
        /// 获取角色成员
        /// </summary>
        /// <param name="RoleId">角色id</param>
        /// <returns></returns>
        public async Task<List<RoleUserInfo>> GetRoleAndUserAsync(Guid RoleId)
        {
            return await _roleAndUserSet.AsNoTracking()
                .Join(_userSet, r => r.UserId, u => u.Id, (r, u) => new RoleUserInfo()
                {
                    Id = r.Id,
                    RoleId = r.RoleId,
                    UserId = r.UserId,
                    Account = u.Account,
                    Name = u.Name
                }).Where(w => w.RoleId == RoleId).OrderBy(w => w.UserId).ToListAsync();
        }

        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        public async Task<RoleAndUser> GetRoleUserAsync(Guid Id)
        {
            return await _roleAndUserSet.AsNoTracking().FirstOrDefaultAsync(w => w.Id == Id);
        }

        /// <summary>
        /// 获取角色用户
        /// </summary>
        /// <param name="RoleId">角色id</param>
        /// <returns></returns>
        public async Task<List<RoleAndUser>> GetRoleUsersAsync(Guid RoleId)
        {
            return await _roleAndUserSet.AsNoTracking().Where(w => w.RoleId == RoleId).ToListAsync();
        }

        /// <summary>
        /// 角色成员
        /// </summary>
        /// <param name="addlist">新增</param>
        /// <param name="dellist">删除</param>
        /// <returns></returns>
        public async Task<bool> RoleUserAsync(List<RoleAndUser> addlist, List<RoleAndUser> dellist)
        {
            try
            {
                using var tran = _context.Database.BeginTransaction();
                if (addlist != null && addlist.Count > 0)
                    _roleAndUserSet.AddRange(addlist);
                if (dellist != null && dellist.Count > 0)
                    _roleAndUserSet.RemoveRange(dellist);
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
        /// 删除成员
        /// </summary>
        /// <param name="Id">成员id</param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleUserAsync(Guid Id)
        {
            var data = await _roleAndUserSet.AsNoTracking().Where(w => w.Id == Id).FirstOrDefaultAsync();
            if (data == null)
                return true;
            _roleAndUserSet.Remove(data);
            return await _context.SaveChangesAsync() > 0;
        }
        #endregion

        #region 角色权限
        /// <summary>
        /// 获取角色权限
        /// </summary>
        /// <returns></returns>
        public async Task<List<RoleAndPermission>> GetRoleAndPermissionsAsync()
        {
            return await _roleAndPermissionSet.AsNoTracking().ToListAsync();
        }
        #endregion
    }
}
