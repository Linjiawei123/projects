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
    public class BSCustomerRepository : DataContext, IBSCustomerRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSCustomer> _bSCustomerSet;
        private readonly DbSet<CUGrade> _cuGradeSet;
        private readonly DbSet<CUState> _cuStateSet;
        private readonly DbSet<CUCredit> _cuCreditSet;
        private readonly DbSet<CUTrade> _cuTradeSet;
        private readonly DbSet<BSEmployee> _bSEmployeeSet;
        public BSCustomerRepository(DataContext context)
        {
            _context = context;
            _bSCustomerSet = _context.Set<BSCustomer>();
            _cuGradeSet = _context.Set<CUGrade>();
            _cuStateSet = _context.Set<CUState>();
            _cuCreditSet = _context.Set<CUCredit>();
            _cuTradeSet = _context.Set<CUTrade>();
            _bSEmployeeSet = _context.Set<BSEmployee>();
        }

        public async Task<PageModel<List<BSCustomer>>> GetPageAsync(string CustomerCode, string CustomerName, short pageSize, int pageIndex)
        {
            if (pageSize <= 0)
                pageSize = 10;
            if (pageIndex <= 0)
                pageIndex = 10;
            IQueryable<BSCustomer> query = GetQuery(CustomerCode, CustomerName);
            int recordCount = await query.CountAsync();
            int pageCount = PublicMethods.GetPageCount(pageSize, recordCount);
            List<BSCustomer> pageData = await query.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToListAsync();
            return new PageModel<List<BSCustomer>> { RecordCount = recordCount, PageCount = pageCount, PageData = pageData };
        }

        private IQueryable<BSCustomer> GetQuery(string CustomerCode, string CustomerName)
        {
            Expression<Func<BSCustomer, bool>> exp = w => true;
            if (!string.IsNullOrEmpty(CustomerCode))
                exp = exp.And(w => w.CustomerCode == CustomerCode);
            if (!string.IsNullOrEmpty(CustomerName))
                exp = exp.And(w => w.CustomerName.Contains(CustomerName));

            return _bSCustomerSet.AsNoTracking().Where(exp).OrderBy(w => w.CustomerCode);
        }


        public async Task<BSCustomer> GetAsync(long Id)
        {
            return await _bSCustomerSet.AsNoTracking().Where(w => w.Id == Id).FirstOrDefaultAsync();
        }

        public async Task<bool> AddAsync(BSCustomer obj)
        {
            _bSCustomerSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateAsync(BSCustomer obj)
        {
            _context.Attach(obj);
            _context.Entry(obj).State = EntityState.Modified;
            _context.Entry(obj).Property(e => e.Id).IsModified = false;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(BSCustomer obj)
        {
            _bSCustomerSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> AnyAsync(string CustomerCode)
        {
            return await _bSCustomerSet.AnyAsync(w => w.CustomerCode == CustomerCode);
        }

        public async Task<CUTypeSimple> GetOtherAsync()
        {
            var cuTypeSimple = new CUTypeSimple
            {
                Grade = await _cuGradeSet.OrderBy(w => w.GradeCode).ToListAsync(),
                State = await _cuStateSet.OrderBy(w => w.StateCode).ToListAsync(),
                Credit = await _cuCreditSet.OrderBy(c => c.CreditCode).ToListAsync(),
                Trade = await _cuTradeSet.OrderBy(w => w.TradeCode).ToListAsync(),
                Employees = await _bSEmployeeSet.OrderBy(w => w.EmployeeCode).ToListAsync()
            };

            return cuTypeSimple;
        }
    }
}
