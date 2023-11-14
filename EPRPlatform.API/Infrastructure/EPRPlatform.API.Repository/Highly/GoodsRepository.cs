using EPRPlatform.API.Interfaces.Highly;
using EPRPlatform.API.Models.Highly;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Repository.Highly
{
    public class GoodsRepository : DataContext, IGoodsRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Goods> _goodsSet;
        private readonly DbSet<GoodsOrder> _orderSet;
        public GoodsRepository(DataContext context)
        {
            _context = context;
            _goodsSet = _context.Set<Goods>();
            _orderSet = _context.Set<GoodsOrder>();
        }
        public async Task<Goods> Get(Guid Id)
        {
            return await _goodsSet.AsNoTracking().FirstOrDefaultAsync(w => w.Id == Id);
        }

        public async Task<bool> Update(Goods goods,GoodsOrder goodsOrder)
        {
            using var tran = _context.Database.BeginTransaction();
            _context.Attach(goods);
            _context.Entry(goods).State = EntityState.Modified;
            _orderSet.Add(goodsOrder);
            await _context.SaveChangesAsync();
            await tran.CommitAsync();
            return true;
        }
    }
}
