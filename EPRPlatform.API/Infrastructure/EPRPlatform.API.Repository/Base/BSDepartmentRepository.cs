using EPRPlatform.API.Dto.BaseModels;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace EPRPlatform.API.Repository
{
    public class BSDepartmentRepository : DataContext, IBSDepartmentRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<BSDepartment> _bSDepartmentSet;
        public BSDepartmentRepository(DataContext context)
        {
            _context = context;
            _bSDepartmentSet = _context.Set<BSDepartment>();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<BSDepartmentSimple>> GetListAsync()
        {
            return await _bSDepartmentSet.AsNoTracking().Include(w => w.BSEmployees).Select(w => new BSDepartmentSimple { Id = w.Id, DepartmentCode = w.DepartmentCode, DepartmentName = w.DepartmentName, IsHasEmployee = w.BSEmployees != null && w.BSEmployees.Count > 0 }).ToListAsync();
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        public async Task<BSDepartment> GetAsync(int Id)
        {
            return await _bSDepartmentSet.AsNoTracking().Include(w => w.BSEmployees).FirstOrDefaultAsync(w => w.Id == Id);
        }

        /// <summary>
        /// 判断编号是否存在
        /// </summary>
        /// <param name="DepartmentCode">编号</param>
        /// <returns></returns>
        public async Task<bool> AnyAsync(string DepartmentCode)
        {
            return await _bSDepartmentSet.AnyAsync(w => w.DepartmentCode == DepartmentCode);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(BSDepartment obj)
        {
            _bSDepartmentSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(BSDepartment obj)
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
        public async Task<bool> DeleteAsync(BSDepartment obj)
        {
            _bSDepartmentSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
