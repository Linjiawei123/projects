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
    public class BSSupplierRepository : DataContext, IBSSupplierRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSSupplier> _bSSupplierSet;
        public BSSupplierRepository(DataContext context)
        {
            _context = context;
            _bSSupplierSet = _context.Set<BSSupplier>();
        }

        public async Task<PageModel<List<BSSupplier>>> GetPageAsync(string SupplierCode, string SupplierName, string TelephoneCode,
            string Email, string PostCode, string Linkman, string Url, string Address, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 10;
            IQueryable<BSSupplier> query = GetQuery(SupplierCode, SupplierName, TelephoneCode,
            Email, PostCode, Linkman, Url, Address);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<BSSupplier> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<BSSupplier>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<BSSupplier> GetQuery(string SupplierCode, string SupplierName, string TelephoneCode,
            string Email, string PostCode, string Linkman, string Url, string Address)
        {
            Expression<Func<BSSupplier, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(SupplierCode))
                exp = exp.And(w => w.SupplierCode == SupplierCode);
            if (!string.IsNullOrEmpty(SupplierName))
                exp = exp.And(w => w.SupplierName.Contains(SupplierName));
            if (!string.IsNullOrEmpty(TelephoneCode))
                exp = exp.And(w=>w.TelephoneCode == TelephoneCode);
            if (!string.IsNullOrEmpty(Email))
                exp = exp.And(w=>w.Email == Email);
            if (!string.IsNullOrEmpty(PostCode))
                exp = exp.And(exp => exp.PostCode == PostCode);
            if (!string.IsNullOrEmpty(Linkman))
                exp = exp.And(w=>w.Linkman.Contains(Linkman));
            if (!string.IsNullOrEmpty(Address))
                exp = exp.And(w=> w.Address.Contains(Address));

            return _bSSupplierSet.AsNoTracking().Where(exp).OrderBy(w => w.SupplierCode);
        }


        public async Task<BSSupplier> GetAsync(long Id)
        {
            return await _bSSupplierSet.AsNoTracking().Where(w => w.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(BSSupplier obj)
        {
            _bSSupplierSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BSSupplier obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(BSSupplier obj)
        {
            _bSSupplierSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyAsync(string SupplierCode)
        {
            return await _bSSupplierSet.AnyAsync(w => w.SupplierCode == SupplierCode);
        }
    }
}
