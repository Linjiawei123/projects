
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
    public class BSInvenTypeRepository : DataContext, IBSInvenTypeRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSInvenType> _bSInvenTypeSet;
        public BSInvenTypeRepository(DataContext context)
        {
            _context = context;
            _bSInvenTypeSet = _context.Set<BSInvenType>();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<BSInvenTypeSimple>> GetListAsync()
        {
            return await _bSInvenTypeSet.AsNoTracking().Include(w => w.BSInvens).Select(w => new BSInvenTypeSimple { Id = w.Id, InvenTypeCode = w.InvenTypeCode, InvenTypeName = w.InvenTypeName, IsHasInven = w.BSInvens != null && w.BSInvens.Count > 0 }).ToListAsync();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        public async Task<BSInvenType> GetAsync(int Id)
        {
            return await _bSInvenTypeSet.AsNoTracking().Include(w => w.BSInvens).FirstOrDefaultAsync(w => w.Id == Id);
        }

        /// <summary>
        /// 判断编号是否存在
        /// </summary>
        /// <param name="InvenTypeCode">编号</param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(string InvenTypeCode)
        {
            return await _bSInvenTypeSet.AnyAsync(w => w.InvenTypeCode == InvenTypeCode);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(BSInvenType obj)
        {
            _bSInvenTypeSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(BSInvenType obj)
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
        public async Task<bool> DeleteAsync(BSInvenType obj)
        {
            _bSInvenTypeSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
