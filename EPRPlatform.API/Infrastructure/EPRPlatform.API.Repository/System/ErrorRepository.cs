using EPRPlatform.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPRPlatform.API.Interfaces;

namespace EPRPlatform.API.Repository
{
    public class ErrorRepository : DataContext, IErrorRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<ErrorLog> _errorSet;
        public ErrorRepository(DataContext context)
        {
            _context = context;
            _errorSet = _context.Set<ErrorLog>();
        }

        public async Task AddErrorAsync(Exception ex)
        {
            ErrorLog log = new ErrorLog()
            {
                Message = ex.Message,
                Source = ex.Source,
                StackTrace = ex.StackTrace,
                AddTime = DateTime.Now
            };
            if (log.Message.Length > 500)
                log.Message = log.Message.Substring(0, 500);
            _errorSet.Add(log);
            await _context.SaveChangesAsync();
        }
    }
}
