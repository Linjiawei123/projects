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
    public class BSStoreRepository : DataContext, IBSStoreRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSStore> _bSStoreSet;
        private readonly DbSet<BSEmployee> _bSEmployeeSet;
        public BSStoreRepository(DataContext context)
        {
            _context = context;
            _bSStoreSet = _context.Set<BSStore>();
            _bSEmployeeSet = _context.Set<BSEmployee>();
        }

        public async Task<PageModel<List<BSStoreSimple>>> GetPageAsync(string StoreCode, string StoreName, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 10;
            IQueryable<BSStoreSimple> query = GetQuery(StoreCode, StoreName);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<BSStoreSimple> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<BSStoreSimple>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<BSStoreSimple> GetQuery(string StoreCode, string StoreName)
        {
            Expression<Func<BSStore, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(StoreCode))
                exp = exp.And(w => w.StoreCode == StoreCode);
            if (!string.IsNullOrEmpty(StoreName))
                exp = exp.And(w => w.StoreName.Contains(StoreName));
            return _bSStoreSet.AsNoTracking().Where(exp)
                .Join(_bSEmployeeSet, p => p.EmployeeCode, q => q.EmployeeCode, (p, q) => new BSStoreSimple
                {
                    Id = p.Id,
                    StoreCode = p.StoreCode,
                    StoreName = p.StoreName,
                    Area = p.Area,
                    EmployeeCode = p.EmployeeCode,
                    EmployeeName = q.EmployeeName,
                    Remark = p.Remark
                }).OrderBy(w => w.StoreCode);
        }


        public async Task<BSStoreSimple> GetAsync(long Id)
        {
            return await _bSStoreSet.AsNoTracking().Where(w => w.Id == Id)
                 .Join(_bSEmployeeSet, p => p.EmployeeCode, q => q.EmployeeCode, (p, q) => new BSStoreSimple
                 {
                     Id = p.Id,
                     StoreCode = p.StoreCode,
                     StoreName = p.StoreName,
                     Area = p.Area,
                     EmployeeCode = p.EmployeeCode,
                     EmployeeName = q.EmployeeName,
                     Remark = p.Remark
                 }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(BSStore obj)
        {
            _bSStoreSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BSStore obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property(e => e.Id).IsModified = false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(BSStore obj)
        {
            _bSStoreSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyAsync(string StoreCode)
        {
            return await _bSStoreSet.AnyAsync(w => w.StoreCode == StoreCode);
        }

        public async Task<List<BSEmployee>> GetEmployeesAsync()
        {
            return await _bSEmployeeSet.AsNoTracking().ToListAsync(); 
        }
    }
}
