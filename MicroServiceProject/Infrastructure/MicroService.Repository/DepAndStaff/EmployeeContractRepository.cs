using Microsoft.EntityFrameworkCore;
using MicroService.Models;
using MicroService.Method;
using MicroService.Method.EFCore;
using MicroService.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Repository
{
    internal class EmployeeContractRepository : DbContext, IEmployeeContractRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Employee> _employeeSet;
        private readonly DbSet<EmployeeContract> _contractSet;
        public EmployeeContractRepository(DataContext context)
        {
            _context = context;
            _employeeSet = _context.Set<Employee>();
            _contractSet = _context.Set<EmployeeContract>();
        }
        /// <summary>
        /// 获取合同分页
        /// </summary>
        /// <param name="name">职工姓名</param>
        /// <param name="code">合同编号</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        public async Task<PageModel<List<EmployeeContractSimple>>> GetPageAsync(string name, string code, short pageSize, int pageIndex)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageIndex <= 0) pageIndex = 0;
            IQueryable<EmployeeContractSimple> query = GetQuery(name, code);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<EmployeeContractSimple> pageData = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
            return new PageModel<List<EmployeeContractSimple>>() { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<EmployeeContractSimple> GetQuery(string name, string code)
        {
            Expression<Func<EmployeeContract, bool>> exp = w => true;
            Expression<Func<Employee, bool>> exp2 = w => true;
            if (!string.IsNullOrEmpty(name))
                exp2 = exp2.And(w => w.Name.Contains(name));
            if (!string.IsNullOrEmpty(code))
                exp = exp.And(w => w.Code == code);
            return _contractSet
                .AsNoTracking()
                .NotDeleted()
                .Where(exp)
                .Join(_employeeSet.Where(exp2), p => p.EmployeeId, q => q.Id, (p, q) =>
                new EmployeeContractSimple
                {
                    Id = p.Id,
                    Name = q.Name,
                    Code = p.Code,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Salary = p.Salary,
                    Agree = p.Agree,
                    SigningDate = p.SigningDate,
                    FilePath = p.FilePath,
                    OperaterId = p.OperaterId,
                    OperateTime = p.OperateTime,
                    IsDeleted = p.IsDeleted,
                    EmployeeId = p.EmployeeId
                })
                .OrderBy(w => w.Code);
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="id">合同id</param>
        /// <returns></returns>
        public async Task<EmployeeContractSimple> GetAsync(Guid id)
        {
            return await _contractSet
                .AsNoTracking()
                .Where(w => w.Id == id)
                .NotDeleted()
                .Join(_employeeSet, p => p.EmployeeId, q => q.Id, (p, q) =>
                new EmployeeContractSimple
                {
                    Id = p.Id,
                    Name = q.Name,
                    Code = p.Code,
                    StartDate = p.StartDate,
                    EndDate = p.EndDate,
                    Salary = p.Salary,
                    Agree = p.Agree,
                    SigningDate = p.SigningDate,
                    FilePath = p.FilePath,
                    OperaterId = p.OperaterId,
                    OperateTime = p.OperateTime,
                    IsDeleted = p.IsDeleted,
                    EmployeeId = p.EmployeeId
                })
                .OrderBy(w => w.Code)
                .FirstOrDefaultAsync();
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(EmployeeContract obj)
        {
            _contractSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(EmployeeContract obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(EmployeeContract obj)
        {
            _context.SoftDelete(obj);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
