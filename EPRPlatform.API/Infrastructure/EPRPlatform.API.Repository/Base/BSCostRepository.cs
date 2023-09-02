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
    public class BSCostRepository : DataContext, IBSCostRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSCost> _bSCostSet;
        private readonly DbSet<BSCostType> _bSCostTypeSet;
        public BSCostRepository(DataContext context)
        {
            _context = context;
            _bSCostSet = _context.Set<BSCost>();
            _bSCostTypeSet = _context.Set<BSCostType>();
        }

        public async Task<PageModel<List<BSCostSimple>>> GetPageAsync(string CostCode, string CostName, string CostTypeCode, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 10;
            IQueryable<BSCostSimple> query = GetQuery(CostCode, CostName, CostTypeCode);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<BSCostSimple> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<BSCostSimple>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<BSCostSimple> GetQuery(string CostCode, string CostName, string CostTypeCode)
        {
            Expression<Func<BSCost, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(CostCode))
                exp = exp.And(w => w.CostCode == CostCode);
            if (!string.IsNullOrEmpty(CostName))
                exp = exp.And(w => w.CostName.Contains(CostName));
            if (!string.IsNullOrEmpty(CostTypeCode))
                exp = exp.And(w => w.CostTypeCode == CostTypeCode);
            return _bSCostSet.AsNoTracking().Where(exp)
                .Join(_bSCostTypeSet, p => p.CostTypeCode, q => q.CostTypeCode, (p, q) => new BSCostSimple
                {
                    Id = p.Id,
                    CostCode = p.CostCode,
                    CostName = p.CostName,
                    CostTypeCode = p.CostTypeCode,
                    CostTypeName = q.CostTypeName,
                    Remark = p.Remark
                }).OrderBy(w => w.CostCode);
        }


        public async Task<BSCostSimple> GetAsync(long Id)
        {
            return await _bSCostSet.AsNoTracking().Where(w => w.Id == Id)
                 .Join(_bSCostTypeSet, p => p.CostTypeCode, q => q.CostTypeCode, (p, q) => new BSCostSimple
                 {
                     Id = p.Id,
                     CostCode = p.CostCode,
                     CostName = p.CostName,
                     CostTypeCode = p.CostTypeCode,
                     CostTypeName = q.CostTypeName,
                     Remark = p.Remark
                 }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(BSCost obj)
        {
            _bSCostSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BSCost obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property(e => e.Id).IsModified = false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(BSCost obj)
        {
            _bSCostSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyAsync(string CostCode)
        {
            return await _bSCostSet.AnyAsync(w => w.CostCode == CostCode);
        }
    }
}
