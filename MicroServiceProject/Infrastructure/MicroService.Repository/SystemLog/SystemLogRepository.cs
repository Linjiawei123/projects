using Microsoft.EntityFrameworkCore;
using MicroService.Models;
using MicroService.Method;
using MicroService.Method.EFCore;
using MicroService.Interfaces;
using System.Linq.Expressions;

namespace MicroService.Repository
{
    internal class SystemLogRepository : DbContext, ISystemLogRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<User> _userSet;
        private readonly DbSet<UserLoginLog> _loginLogSet;
        private readonly DbSet<OperateLog> _operateLogSet;
        private readonly DbSet<OperateModule> _operateModuleSet;
        public SystemLogRepository(DataContext context)
        {
            _context = context;
            _userSet = _context.Set<User>();
            _loginLogSet = _context.Set<UserLoginLog>();
            _operateLogSet = _context.Set<OperateLog>();
            _operateModuleSet = _context.Set<OperateModule>();
        }

        /// <summary>
        /// 获取用户登录日志
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="name">昵称</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<PageModel<List<UserLoginLogSimple>>> GetLoginLogPageAsync(string account, string name, DateTime? beginTime, DateTime? endTime, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 1;
            IQueryable<UserLoginLogSimple> query = GetLoginLog(account, name, beginTime, endTime);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<UserLoginLogSimple> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<UserLoginLogSimple>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<UserLoginLogSimple> GetLoginLog(string account, string name, DateTime? beginTime, DateTime? endTime)
        {
            Expression<Func<User, bool>> userExp = w => true;
            Expression<Func<UserLoginLog, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(account))
                userExp = userExp.And(w => w.Account == account);
            if (!string.IsNullOrEmpty(name))
                userExp = userExp.And(w => w.Name.Contains(name));
            if (beginTime.HasValue)
                exp = exp.And(w => w.LoginTime > beginTime.Value);
            if (endTime.HasValue)
                exp = exp.And(w => w.LoginTime < endTime.Value);
            return _loginLogSet.AsTracking().Where(exp)
                .Join(_userSet.Where(userExp), p => p.UserId, q => q.Id, (p, q) => new UserLoginLogSimple { Id = p.Id, Account = q.Account, Name = q.Name, LoginTime = p.LoginTime, LoginIP = p.LoginIP })
                .OrderByDescending(w => w.LoginTime);
        }

        /// <summary>
        /// 获取用户操作日志
        /// </summary>
        /// <param name="account">账号</param>
        /// <param name="name">昵称</param>
        /// <param name="beginTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<PageModel<List<OperateLogSimple>>> GetOperateLogPageAsync(string account, string name, DateTime? beginTime, DateTime? endTime, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 1;
            IQueryable<OperateLogSimple> query = GetOperateLog(account, name, beginTime, endTime);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<OperateLogSimple> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<OperateLogSimple>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<OperateLogSimple> GetOperateLog(string account, string name, DateTime? beginTime, DateTime? endTime)
        {
            Expression<Func<User, bool>> userExp = w => true;
            Expression<Func<OperateLog, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(account))
                userExp = userExp.And(w => w.Account == account);
            if (!string.IsNullOrEmpty(name))
                userExp = userExp.And(w => w.Name.Contains(name));
            if (beginTime.HasValue)
                exp = exp.And(w => w.OperateTime > beginTime.Value);
            if (endTime.HasValue)
                exp = exp.And(w => w.OperateTime < endTime.Value);
            return _operateLogSet.AsTracking().Where(exp)
                .Join(_userSet.Where(userExp), p => p.UserId, q => q.Id, (p, q) =>
                new OperateLogSimple { Id = p.Id, Account = q.Account, Name = q.Name, OperateTime = p.OperateTime, Module = p.Module, Method = p.Method, Operate = p.Operate, Path = p.Path })
                .Join(_operateModuleSet, q => q.Module, m => m.Code, (q, m) =>
                new OperateLogSimple { Id = q.Id, Account = q.Account, Name = q.Name, OperateTime = q.OperateTime, Module = q.Module, ModuleName = m.Module, Method = q.Method, Operate = q.Operate, Path = q.Path })
                .OrderByDescending(w => w.OperateTime);
        }

        /// <summary>
        /// 添加用户操作日志
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddOperateLogAsync(OperateLog obj)
        {
            _operateLogSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
