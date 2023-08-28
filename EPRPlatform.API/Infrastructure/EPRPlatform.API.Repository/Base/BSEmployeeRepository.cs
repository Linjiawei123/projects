using AngleSharp.Dom;
using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Method;
using EPRPlatform.API.Method.EFCore;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace EPRPlatform.API.Repository
{
    public class BSEmployeeRepository : DataContext, IBSEmployeeRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSEmployee> _bSEmployeeSet;
        private readonly DbSet<INEduLevel> _iNEduLevelSet;
        private readonly DbSet<INSex> _iNSexSet;
        private readonly DbSet<BSDepartment> _bsDepartmentSet;
        public BSEmployeeRepository(DataContext context)
        {
            _context = context;
            _bSEmployeeSet = _context.Set<BSEmployee>();
            _iNEduLevelSet = _context.Set<INEduLevel>();
            _iNSexSet = _context.Set<INSex>();
            _bsDepartmentSet = _context.Set<BSDepartment>();
        }

        public async Task<PageModel<List<BSEmployeeSimple>>> GetPageAsync(string EmployeeCode, string EmployeeName, string TelephoneCode, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 10;
            IQueryable<BSEmployeeSimple> query = GetQuery(EmployeeCode, EmployeeName, TelephoneCode);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<BSEmployeeSimple> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<BSEmployeeSimple>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<BSEmployeeSimple> GetQuery(string EmployeeCode, string EmployeeName, string TelephoneCode)
        {
            Expression<Func<BSEmployee, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(EmployeeCode))
                exp = exp.And(w => w.EmployeeCode == EmployeeCode);
            if (!string.IsNullOrEmpty(EmployeeName))
                exp = exp.And(w => w.EmployeeName.Contains(EmployeeName));
            if (!string.IsNullOrEmpty(TelephoneCode))
                exp = exp.And(w => w.TelephoneCode == TelephoneCode);
            return _bSEmployeeSet.AsNoTracking().Where(exp)
                .GroupJoin(_bsDepartmentSet, p => p.DepartmentCode, q => q.DepartmentCode,
                (p, q) => new { Employee = p, DepartmentGroup = q })
                .SelectMany(x => x.DepartmentGroup.DefaultIfEmpty(), (x, department) => new BSEmployeeSimple
                {
                    Id = x.Employee.Id,
                    EmployeeCode = x.Employee.EmployeeCode,
                    EmployeeName = x.Employee.EmployeeName,
                    DepartmentCode = x.Employee.DepartmentCode,
                    TelephoneCode = x.Employee.TelephoneCode,
                    Age = x.Employee.Age,
                    Sex = x.Employee.Sex,
                    EduLevel = x.Employee.EduLevel,
                    Job = x.Employee.Job,
                    JoinDate = x.Employee.JoinDate,
                    Remark = x.Employee.Remark,
                    DepartmentName = department != null ? department.DepartmentName : ""
                }).OrderBy(w => w.EmployeeCode);
        }


        public async Task<BSEmployeeSimple> GetAsync(long Id)
        {
            return await _bSEmployeeSet.AsNoTracking().Where(w => w.Id == Id)
                .GroupJoin(_bsDepartmentSet, p => p.DepartmentCode, q => q.DepartmentCode,
                (p, q) => new { Employee = p, DepartmentGroup = q })
                .SelectMany(x => x.DepartmentGroup.DefaultIfEmpty(), (x, department) => new BSEmployeeSimple
                {
                    Id = x.Employee.Id,
                    EmployeeCode = x.Employee.EmployeeCode,
                    EmployeeName = x.Employee.EmployeeName,
                    DepartmentCode = x.Employee.DepartmentCode,
                    TelephoneCode = x.Employee.TelephoneCode,
                    Age = x.Employee.Age,
                    Sex = x.Employee.Sex,
                    EduLevel = x.Employee.EduLevel,
                    Job = x.Employee.Job,
                    JoinDate = x.Employee.JoinDate,
                    Remark = x.Employee.Remark,
                    DepartmentName = department != null ? department.DepartmentName : ""
                }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(BSEmployee obj)
        {
            _bSEmployeeSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BSEmployee obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property(e => e.Id).IsModified = false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(BSEmployee obj)
        {
            _bSEmployeeSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyAsync(string EmployeeCode)
        {
            return await _bSEmployeeSet.AnyAsync(w => w.EmployeeCode == EmployeeCode);
        }

        public async Task<INSimple> GetOtherAsync()
        {
            return new INSimple
            {
                eduLevel = await _iNEduLevelSet.ToListAsync(),
                sex = await _iNSexSet.ToListAsync(),
                department = await _bsDepartmentSet.ToListAsync()
            };
        }
    }
}
