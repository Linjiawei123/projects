using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Method;
using EPRPlatform.API.Method.EFCore;
using EPRPlatform.API.Models;
using EPRPlatform.API.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Repository
{
    public class BSInvenRepository : DataContext, IBSInvenRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSInven> _bSInvenSet;
        private readonly DbSet<BSInvenType> _bSInvenTypeSet;
        public BSInvenRepository(DataContext context)
        {
            _context = context;
            _bSInvenSet = _context.Set<BSInven>();
            _bSInvenTypeSet = _context.Set<BSInvenType>();
        }

        public async Task<PageModel<List<BSInvenSimple>>> GetPageAsync(string InvenCode, string InvenName, string InvenTypeCode, string SpecsModel, string MeaUnit, decimal? SelPrice, decimal? PurPrice, int? SmallStockNum, int? BigStockNum, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 10;
            IQueryable<BSInvenSimple> query = GetQuery(InvenCode, InvenName, InvenTypeCode, SpecsModel, MeaUnit, SelPrice, PurPrice, SmallStockNum, BigStockNum);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<BSInvenSimple> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<BSInvenSimple>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<BSInvenSimple> GetQuery(string InvenCode, string InvenName, string InvenTypeCode, string SpecsModel, string MeaUnit, decimal? SelPrice, decimal? PurPrice, int? SmallStockNum, int? BigStockNum)
        {
            Expression<Func<BSInven, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(InvenCode))
                exp = exp.And(w => w.InvenCode == InvenCode);
            if (!string.IsNullOrEmpty(InvenName))
                exp = exp.And(w => w.InvenName.Contains(InvenName));
            if (!string.IsNullOrEmpty(InvenTypeCode))
                exp = exp.And(w => w.InvenTypeCode == InvenTypeCode);
            if (!string.IsNullOrEmpty(SpecsModel))
                exp = exp.And(w => w.SpecsModel.Contains(SpecsModel));
            if (!string.IsNullOrEmpty(MeaUnit))
                exp = exp.And(w => w.MeaUnit.Contains(MeaUnit));
            if (SelPrice.HasValue)
                exp = exp.And(w => w.SelPrice == SelPrice);
            if (PurPrice.HasValue)
                exp = exp.And(w => w.PurPrice == PurPrice);
            if (SmallStockNum.HasValue)
                exp = exp.And(w => w.SmallStockNum == SmallStockNum);
            if (BigStockNum.HasValue)
                exp = exp.And(w => w.BigStockNum == BigStockNum);
            return _bSInvenSet.AsNoTracking().Where(exp)
                .Join(_bSInvenTypeSet, p => p.InvenTypeCode, q => q.InvenTypeCode, (p, q) => new BSInvenSimple
                {
                    Id = p.Id,
                    InvenCode = p.InvenCode,
                    InvenName = p.InvenName,
                    InvenTypeCode = p.InvenTypeCode,
                    SpecsModel = p.SpecsModel,
                    MeaUnit = p.MeaUnit,
                    SelPrice = p.SelPrice,
                    PurPrice = p.PurPrice,
                    SmallStockNum = p.SmallStockNum,
                    BigStockNum = p.BigStockNum,
                    InvenTypeName = q.InvenTypeName
                }).OrderBy(w => w.InvenCode);
        }


        public async Task<BSInvenSimple> GetAsync(long Id)
        {
            return await _bSInvenSet.AsNoTracking().Where(w => w.Id == Id)
                 .Join(_bSInvenTypeSet, p => p.InvenTypeCode, q => q.InvenTypeCode, (p, q) => new BSInvenSimple
                 {
                     Id = p.Id,
                     InvenCode = p.InvenCode,
                     InvenName = p.InvenName,
                     InvenTypeCode = p.InvenTypeCode,
                     SpecsModel = p.SpecsModel,
                     MeaUnit = p.MeaUnit,
                     SelPrice = p.SelPrice,
                     PurPrice = p.PurPrice,
                     SmallStockNum = p.SmallStockNum,
                     BigStockNum = p.BigStockNum,
                     InvenTypeName = q.InvenTypeName
                 }).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(BSInven obj)
        {
            _bSInvenSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BSInven obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property(e => e.Id).IsModified = false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(BSInven obj)
        {
            _bSInvenSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyAsync(string InvenCode)
        {
            return await _bSInvenSet.AnyAsync(w => w.InvenCode == InvenCode);
        }
    }
}
