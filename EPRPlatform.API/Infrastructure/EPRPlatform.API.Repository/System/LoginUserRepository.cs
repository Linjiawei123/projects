using Microsoft.EntityFrameworkCore;
using EPRPlatform.API.Models;
using EPRPlatform.API.Interfaces;

namespace EPRPlatform.API.Repository
{
    public class LoginUserRepository : DbContext, ILoginUserRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<User> _userSet;
        private readonly DbSet<Menu> _menuSet;
        private readonly DbSet<UserLogin> _userLoginSet;
        private readonly DbSet<UserRights> _userRightsSet;
        private readonly DbSet<UserLoginLog> _logSet;
        public LoginUserRepository(DataContext context)
        {
            _context = context;
            _userSet = _context.Set<User>();
            _menuSet = _context.Set<Menu>();
            _userLoginSet = _context.Set<UserLogin>();
            _userRightsSet = _context.Set<UserRights>();
            _logSet = _context.Set<UserLoginLog>();
        }
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="Id">用户Id</param>
        /// <returns></returns>
        public async Task<User> GetUserAsync(Guid Id)
        {
            return await _userSet.AsNoTracking().FirstOrDefaultAsync(w => w.Id == Id);
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
        /// 登录失败记录
        /// </summary>
        /// <param name="obj">用户实体</param>
        /// <returns></returns>
        public async Task<bool> LoginErrAsync(User obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).Property(q => q.Status).IsModified = true;
            _context.Entry(obj).Property(q => q.ErrTimes).IsModified = true;
            _context.Entry(obj).Property(q => q.FirstErrTime).IsModified = true;
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 获取登录信息
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public async Task<UserLogin> GetAsync(Guid UserId)
        {
            return await _userLoginSet.AsNoTracking().FirstOrDefaultAsync(w => w.UserId == UserId);
        }
        /// <summary>
        /// 新增登录信息
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="log">日志</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(UserLogin obj, UserLoginLog log)
        {
            using var tran = _context.Database.BeginTransaction();
            _userLoginSet.Add(obj);
            _logSet.Add(log);
            await _context.SaveChangesAsync();
            await tran.CommitAsync();
            return true;
        }
        /// <summary>
        /// 修改登录信息
        /// </summary>
        /// <param name="obj">实体</param>
        /// <param name="user">用户</param>
        /// <param name="log">日志</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(UserLogin obj, User user, UserLoginLog log)
        {
            using var tran = _context.Database.BeginTransaction();
            _context.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            if (user != null)
            {
                _context.Attach(user);
                _context.Entry(user).Property(q => q.Status).IsModified = true;
                _context.Entry(user).Property(q => q.ErrTimes).IsModified = true;
                _context.Entry(user).Property(q => q.FirstErrTime).IsModified = true;
            }
            if (log != null)
                _logSet.Add(log);
            await _context.SaveChangesAsync();
            await tran.CommitAsync();
            return true;
        }
        /// <summary>
        /// 根据token获取登录用户
        /// </summary>
        /// <param name="token">登录token</param>
        /// <returns></returns>
        public async Task<UserLogin> GetUserLoginAsync(string token)
        {
            return await _userLoginSet.AsNoTracking().FirstOrDefaultAsync(w => w.Access_token == token);
        }
        /// <summary>
        /// 获取用户权限
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public async Task<List<UserRights>> GetUserRightsAsync(Guid UserId)
        {
            var list = await _userRightsSet.AsNoTracking().Where(w => w.UserId == UserId).ToListAsync();
            if (list != null && list.Count > 0)
                return list;
            else
                return new List<UserRights>();
        }
        /// <summary>
        /// 获取用户菜单
        /// </summary>
        /// <param name="UserId">用户Id</param>
        /// <returns></returns>
        public async Task<List<Menu>> GetUserMenusAsync(Guid UserId)
        {
            var rlist = await _userRightsSet.AsNoTracking().Where(w => w.UserId == UserId).ToListAsync();
            if (rlist != null && rlist.Count > 0)
            {
                var mlist = rlist.Select(w => w.MenuId).ToList();
                if (mlist.Count > 0)
                {
                    return await _menuSet.AsNoTracking().Where(w => mlist.Contains(w.Id)).OrderBy(w => w.Sort).ToListAsync();
                }
            }
            return new List<Menu>();
        }
        /// <summary>
        /// 根据刷新token获取登录信息
        /// </summary>
        /// <param name="RefreshToken">刷新token</param>
        /// <returns></returns>
        public async Task<UserLogin> GetRefreshTokenAsync(string RefreshToken)
        {
            return await _userLoginSet.AsNoTracking().FirstOrDefaultAsync(w => w.Refresh_token == RefreshToken);
        }
    }
}
