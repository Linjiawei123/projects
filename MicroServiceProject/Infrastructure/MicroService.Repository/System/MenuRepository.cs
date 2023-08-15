using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MicroService.Models;
using MicroService.Interfaces;
using System.Linq.Expressions;

namespace MicroService.Repository
{
    public class MenuRepository : DbContext, IMenuRepository
    {
        private readonly DataContext _context;
        private readonly DbSet<Menu> _menuSet;
        private readonly DbSet<Permission> _permissionSet;
        public MenuRepository(DataContext context)
        {
            _context = context;
            _menuSet = _context.Set<Menu>();
            _permissionSet = _context.Set<Permission>();
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<Menu>> GetListAsync()
        {
            Expression<Func<Menu, bool>> exp = w => true;
            return await _menuSet.AsNoTracking().Where(exp).ToListAsync();
        }

        /// <summary>
        /// 获取所有菜单
        /// </summary>
        /// <returns></returns>
        public async Task<List<MenuRights>> GetMenuRightsAsync()
        {
            Expression<Func<Menu, bool>> exp = w => true;
            var ml = await _menuSet.AsNoTracking().Where(exp).ToListAsync();
            var pl = await _permissionSet.AsNoTracking().ToListAsync();
            MapperConfiguration config = new(cfg =>
            {
                cfg.CreateMap<Menu, MenuRights>()
                .ForMember(q => q.Rights, p => p.MapFrom(w => pl.FindAll(e => e.MenuId == w.Id).ToList()));
            });
            IMapper mapper = config.CreateMapper();
            return mapper.Map<List<MenuRights>>(ml);
        }

        /// <summary>
        /// 获取
        /// </summary>
        /// <param name="id">菜单Id</param>
        /// <returns></returns>
        public async Task<Menu> GetByIdAsync(Guid id)
        {
            return await _menuSet.AsNoTracking().Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(Menu obj)
        {
            _menuSet.Add(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> UpdateAsync(Menu obj)
        {
            using (var tran = _context.Database.BeginTransaction())
            {
                _context.Attach(obj);
                _context.Entry(obj).Property(q => q.ParentId).IsModified = true;
                _context.Entry(obj).Property(q => q.Name).IsModified = true;
                _context.Entry(obj).Property(q => q.Code).IsModified = true;
                _context.Entry(obj).Property(q => q.Sort).IsModified = true;
                _context.Entry(obj).Property(q => q.Url).IsModified = true;
                _context.Entry(obj).Property(q => q.Icon).IsModified = true;
                _context.Entry(obj).Property(q => q.Remark).IsModified = true;
                _context.Entry(obj).Property(q => q.Grade).IsModified = true;
                _context.Entry(obj).Property(q => q.IsDefault).IsModified = true;
                _context.Entry(obj).Property(q => q.IsBlank).IsModified = true;
                _context.Entry(obj).Property(q => q.OperaterId).IsModified = true;
                _context.Entry(obj).Property(q => q.OperateTime).IsModified = true;
                await _context.SaveChangesAsync();
                await tran.CommitAsync();
                return true;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="obj">实体</param>
        /// <returns></returns>
        public async Task<bool> DeleteAsync(Menu obj)
        {
            _menuSet.Remove(obj);
            return await _context.SaveChangesAsync() > 0;
        }

        /// <summary>
        /// 编号是否被使用
        /// </summary>
        /// <param name="code">编号</param>
        /// <param name="id">数据id</param>
        /// <returns></returns>
        public async Task<bool> ExistsByCode(string code, Guid? id)
        {
            if(id!=null)
                return await _menuSet.AnyAsync(q => q.Code == code&&q.Id!=id);
            else
                return await _menuSet.AnyAsync(q=>q.Code == code);
        }
        /// <summary>
        /// 是否存在下级
        /// </summary>
        /// <param name="Id">菜单id</param>
        /// <returns></returns>
        public async Task<bool> HasChildAsync(Guid Id)
        {
            return await _menuSet.AnyAsync(q=>q.ParentId == Id);
        }
    }
}
