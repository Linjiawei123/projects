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
    public class BSAccountRepository : DataContext, IBSAccountRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSAccount> _bSAccountSet;
        public BSAccountRepository(DataContext context)
        {
            _context = context;
            _bSAccountSet = _context.Set<BSAccount>();
        }

        public async Task<PageModel<List<BSAccount>>> GetPageAsync(string AccountCode, string AccountName, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 10;
            IQueryable<BSAccount> query = GetQuery(AccountCode, AccountName);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<BSAccount> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<BSAccount>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<BSAccount> GetQuery(string AccountCode, string AccountName)
        {
            Expression<Func<BSAccount, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(AccountCode))
                exp = exp.And(w => w.AccountCode == AccountCode);
            if (!string.IsNullOrEmpty(AccountName))
                exp = exp.And(w => w.AccountName.Contains(AccountName));
            return _bSAccountSet.AsNoTracking().Where(exp).OrderBy(w => w.AccountCode);
        }


        public async Task<BSAccount> GetAsync(long Id)
        {
            return await _bSAccountSet.AsNoTracking().Where(w => w.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(BSAccount obj)
        {
            _bSAccountSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BSAccount obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property(e => e.Id).IsModified = false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(BSAccount obj)
        {
            _bSAccountSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyAsync(string AccountCode)
        {
            return await _bSAccountSet.AnyAsync(w => w.AccountCode == AccountCode);
        }
    }
}
