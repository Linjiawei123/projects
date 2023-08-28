
using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Models.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Repository
{
    public class BSCostTypeRepository : DataContext, IBSCostTypeRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSCostType> _bSCostTypeSet;
        public BSCostTypeRepository(DataContext context)
        {
            _context = context;
            _bSCostTypeSet = _context.Set<BSCostType>();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<BSCostTypeSimple>> GetListAsync()
        {
            return await _bSCostTypeSet.AsNoTracking().Include(w => w.BSCosts).Select(w => new BSCostTypeSimple { Id = w.Id, CostTypeCode = w.CostTypeCode, CostTypeName = w.CostTypeName, IsHasCost = w.BSCosts != null && w.BSCosts.Count > 0 }).ToListAsync();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        public async Task<BSCostType> GetAsync(int Id)
        {
            return await _bSCostTypeSet.AsNoTracking().Include(w => w.BSCosts).FirstOrDefaultAsync(w => w.Id == Id);
        }

        /// <summary>
        /// 判断编号是否存在
        /// </summary>
        /// <param name="CostTypeCode">编号</param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(string CostTypeCode)
        {
            return await _bSCostTypeSet.AnyAsync(w => w.CostTypeCode == CostTypeCode);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(BSCostType obj)
        {
            _bSCostTypeSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(BSCostType obj)
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
        public async Task<bool> DeleteAsync(BSCostType obj)
        {
            _bSCostTypeSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
