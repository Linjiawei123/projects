using MicroService.Interfaces;
using MicroService.Method.EFCore;
using MicroService.Models;
using Microsoft.EntityFrameworkCore;

namespace MicroService.Repository
{
    public class CustomModuleRepository : DataContext, ICustomModuleRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Module> _moduleSet;
        private readonly DbSet<Property> _propertieSet;
        public CustomModuleRepository(DataContext context)
        {
            _context = context;
            _moduleSet = _context.Set<Module>();
            _propertieSet = _context.Set<Property>();
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<Module>> GetListAsync()
        {
            return await _moduleSet.AsNoTracking().ToListAsync();
        }

        /// <summary>
        /// 根基id获取
        /// </summary>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        public async Task<Module> GetByIdAsync(int id)
        {
            return await _moduleSet.AsNoTracking().Include(w => w.CustomPropertys.Where(p => p.IsDeleted == false)).FirstOrDefaultAsync(w => w.Id == id);
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Module obj)
        {
            _moduleSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Module obj)
        {
            _context.Update(obj);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 属性详情
        /// </summary>
        /// <param name="id">属性id</param>
        /// <returns></returns>
        public async Task<Property> GetPropertyAsync(int id)
        {
            return await _propertieSet.AsNoTracking().NotDeleted().FirstOrDefaultAsync(w => w.Id == id);
        }
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddPropertyAsync(Property obj)
        {
            _propertieSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdatePropertyAsync(Property obj)
        {
            _propertieSet.Update(obj);
            return await _context.SaveChangesAsync() > 0;
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> DeletePropertyAsync(Property obj)
        {
            _context.SoftDelete(obj);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
