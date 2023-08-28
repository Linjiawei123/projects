//using AngleSharp.Dom;
using Microsoft.EntityFrameworkCore;
using EPRPlatform.API.Models;
using EPRPlatform.API.Method;
using EPRPlatform.API.Method.EFCore;
using System.Linq.Expressions;

namespace EPRPlatform.API.Repository
{
    internal class EmployeeRepository : DbContext, IEmployeeRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<User> _userSet;
        private readonly DbSet<Employee> _employeeSet;
        private readonly DbSet<Department> _departmentSet;
        public EmployeeRepository(DataContext context)
        {
            _context = context;
            _userSet = _context.Set<User>();
            _employeeSet = _context.Set<Employee>();
            _departmentSet = _context.Set<Department>();
        }
        /// <summary>
        /// 获取职工分页数据
        /// </summary>
        /// <param name="departmentId">部门id</param>
        /// <param name="name">姓名</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<PageModel<List<EmployeeInfo>>> GetPageAsync(Guid? departmentId, string name, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 1;
            IQueryable<EmployeeInfo> query = GetQuery(departmentId, name);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<EmployeeInfo> pageData = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return new PageModel<List<EmployeeInfo>>() { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<EmployeeInfo> GetQuery(Guid? departmentId, string name)
        {
            Expression<Func<Employee, bool>> exp = w => true;
            if (departmentId.HasValue)
                exp = exp.And(w => w.DepartmentId == departmentId);
            if (!string.IsNullOrEmpty(name))
                exp = exp.And(w => w.Name.Contains(name));
            return _employeeSet.AsNoTracking().NotDeleted()
                .Where(exp)
                .Join(_departmentSet, p => p.DepartmentId, q => q.Id, (p, q) => new EmployeeInfo
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Sex = p.Sex,
                    Birthday = p.Birthday,
                    IdentityCard = p.IdentityCard,
                    NativePlace = p.NativePlace,
                    Address = p.Address,
                    Photo = p.Photo,
                    Email = p.Email,
                    Phone = p.Phone,
                    HireDate = p.HireDate,
                    Job = p.Job,
                    Remark = p.Remark,
                    DepartmentId = p.DepartmentId,
                    Department = q.Name,
                    UserId = p.UserId,
                    IsDeleted = p.IsDeleted,
                    OperaterId = p.OperaterId,
                    OperateTime = p.OperateTime,
                    Account = ""
                })
                .GroupJoin(_userSet, p => p.UserId, q => q.Id, (p, q) => new { EmployeeInfo = p, User = q })
                .SelectMany(w => w.User.DefaultIfEmpty(), (p, q) => new EmployeeInfo
                {
                    Id = p.EmployeeInfo.Id,
                    Name = p.EmployeeInfo.Name,
                    Code = p.EmployeeInfo.Code,
                    Sex = p.EmployeeInfo.Sex,
                    Birthday = p.EmployeeInfo.Birthday,
                    IdentityCard = p.EmployeeInfo.IdentityCard,
                    NativePlace = p.EmployeeInfo.NativePlace,
                    Address = p.EmployeeInfo.Address,
                    Photo = p.EmployeeInfo.Photo,
                    Email = p.EmployeeInfo.Email,
                    Phone = p.EmployeeInfo.Phone,
                    HireDate = p.EmployeeInfo.HireDate,
                    Job = p.EmployeeInfo.Job,
                    Remark = p.EmployeeInfo.Remark,
                    DepartmentId = p.EmployeeInfo.DepartmentId,
                    Department = p.EmployeeInfo.Department,
                    UserId = p.EmployeeInfo.UserId,
                    IsDeleted = p.EmployeeInfo.IsDeleted,
                    Account = q.Account,
                    OperaterId = p.EmployeeInfo.OperaterId,
                    OperateTime = p.EmployeeInfo.OperateTime,
                })
                .OrderBy(w => w.Code);
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        public async Task<EmployeeInfo> GetAsync(Guid Id)
        {
            return await _employeeSet.AsNoTracking().NotDeleted()
                .Join(_departmentSet, p => p.DepartmentId, q => q.Id, (p, q) => new EmployeeInfo
                {
                    Id = p.Id,
                    Name = p.Name,
                    Code = p.Code,
                    Sex = p.Sex,
                    Birthday = p.Birthday,
                    IdentityCard = p.IdentityCard,
                    NativePlace = p.NativePlace,
                    Address = p.Address,
                    Photo = p.Photo,
                    Email = p.Email,
                    Phone = p.Phone,
                    HireDate = p.HireDate,
                    Job = p.Job,
                    Remark = p.Remark,
                    DepartmentId = p.DepartmentId,
                    Department = q.Name,
                    UserId = p.UserId,
                    IsDeleted = p.IsDeleted,
                    Account = "",
                    OperaterId = p.OperaterId,
                    OperateTime = p.OperateTime
                })
                .GroupJoin(_userSet, p => p.UserId, q => q.Id, (p, q) => new { EmployeeInfo = p, User = q })
                .SelectMany(w => w.User.DefaultIfEmpty(), (p, q) => new EmployeeInfo
                {
                    Id = p.EmployeeInfo.Id,
                    Name = p.EmployeeInfo.Name,
                    Code = p.EmployeeInfo.Code,
                    Sex = p.EmployeeInfo.Sex,
                    Birthday = p.EmployeeInfo.Birthday,
                    IdentityCard = p.EmployeeInfo.IdentityCard,
                    NativePlace = p.EmployeeInfo.NativePlace,
                    Address = p.EmployeeInfo.Address,
                    Photo = p.EmployeeInfo.Photo,
                    Email = p.EmployeeInfo.Email,
                    Phone = p.EmployeeInfo.Phone,
                    HireDate = p.EmployeeInfo.HireDate,
                    Job = p.EmployeeInfo.Job,
                    Remark = p.EmployeeInfo.Remark,
                    DepartmentId = p.EmployeeInfo.DepartmentId,
                    Department = p.EmployeeInfo.Department,
                    UserId = p.EmployeeInfo.UserId,
                    IsDeleted = p.EmployeeInfo.IsDeleted,
                    Account = q.Account,
                    OperaterId = p.EmployeeInfo.OperaterId,
                    OperateTime = p.EmployeeInfo.OperateTime
                }).FirstOrDefaultAsync(w => w.Id == Id);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="employee">实体</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Employee employee)
        {
            using var tran = _context.Database.BeginTransaction();
            _employeeSet.Add(employee);
            await _context.SaveChangesAsync();
            await tran.CommitAsync();
            return true;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="employee">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Employee employee)
        {
            using var tran = _context.Database.BeginTransaction();
            _context.Entry(employee).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            await tran.CommitAsync();
            return true;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="employee">实体</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Employee employee)
        {
            _context.SoftDelete(employee);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 编号是否已存在
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        public async Task<bool> ExistsCodeAsync(string code, Guid? Id)
        {
            if (Id.HasValue)
                return await _employeeSet.NotDeleted().AnyAsync(w => w.Id != Id && w.Code == code);
            else
                return await _employeeSet.NotDeleted().AnyAsync(w => w.Code == code);
        }
        /// <summary>
        /// 用户是否已被绑定
        /// </summary>
        /// <param name="UserId">用户id</param>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        public async Task<bool> ExistsUserAsync(Guid UserId, Guid? Id)
        {
            if (Id.HasValue)
                return await _employeeSet.NotDeleted().AnyAsync(w => w.Id != Id && w.UserId == UserId);
            else
                return await _employeeSet.NotDeleted().AnyAsync(w => w.UserId == UserId);
        }
        /// <summary>
        /// 获取未绑定职工的用户
        /// </summary>
        /// <returns></returns>
        public async Task<List<User>> GetNotBindUserAsync(Guid? Id)
        {
            Expression<Func<User, bool>> exp = w => true;
            if (Id.HasValue)
                exp = exp.And(w => _employeeSet.NotDeleted().Where(e=>e.Id!=Id).Any(e => e.UserId == w.Id) == false);
            else
                exp = exp.And(w => _employeeSet.NotDeleted().Any(e => e.UserId == w.Id) == false);
            return await _userSet.AsNoTracking().Where(exp).ToListAsync();
        }
    }
}
