
using Microsoft.AspNetCore.Mvc;
using MicroService.Models;
using  MicroService.Dto.OAModels;
using MicroService.Interfaces;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using MicroService.Dto.PublicModels;
using MicroService.Extend;

namespace MicroService.OA.Controllers
{
    /// <summary>
    /// 菜单管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FieldFilter]
    [Authorize]
    public class SysMenuController : BaseController
    {
        private const int _module = 10001;
        private readonly IErrorRepository _iError;
        private readonly IMenuRepository _iMenu;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError">错误数据接口</param>
        /// <param name="iMenu">菜单数据接口</param>
        public SysMenuController(IErrorRepository iError, IMenuRepository iMenu)
        {
            try
            {
                _iError = iError;
                _iMenu = iMenu;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 获取菜单列表
        /// </summary>
        /// <param name="Name">菜单名称</param>
        /// <returns></returns>
        [HttpGet]
        [Permission("Menu_View")]
        public async Task<OutPutModel<List<MenuSimple>>> GetMenusAsync([FromQuery] string Name)
        {
            try
            {
                List<Menu> list = await _iMenu.GetListAsync();
                List<MenuSimple> mlist = new();
                foreach (var item in list)
                {
                    mlist.Add(new MenuSimple()
                    {
                        Id = item.Id,
                        ParentId = item.ParentId,
                        Name = item.Name,
                        Code = item.Code,
                        Sort = item.Sort,
                        Url = item.Url,
                        Icon = item.Icon,
                        Remark = item.Remark,
                        Grade = item.Grade,
                        IsDefault = item.IsDefault,
                        IsBlank = item.IsBlank,
                        Child = new List<MenuSimple>()
                    });
                }
                List<MenuSimple> topList = new();
                List<MenuSimple> childList = new();
                if (string.IsNullOrEmpty(Name))
                {
                    topList = mlist.FindAll(w => w.ParentId == Guid.Empty);//最高级菜单
                    childList = mlist.FindAll(w => w.ParentId != Guid.Empty);//所有子菜单
                }
                else
                {
                    topList = mlist.FindAll(w => w.Name.Contains(Name));//最高级菜单
                }
                foreach (var item in topList)
                {
                    AddChildMenu(item, childList);
                }
                return OutPutMethod<List<MenuSimple>>.Success(topList, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<MenuSimple>>.Failure();
            }
        }

        private void AddChildMenu(MenuSimple parent, List<MenuSimple> childList)
        {
            var childs = childList.FindAll(w => w.ParentId == parent.Id);
            if (childs != null && childs.Count > 0)
            {
                foreach (var item in childs)
                {
                    parent.Child.Add(item);
                    AddChildMenu(item, childList);
                }
            }
        }

        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        [HttpPost("MenuRights")]
        [Permission("Menu_View")]
        public async Task<OutPutModel<List<MenuRightsSimple>>> GetMenuRightsAsync([FromBody] RightsIds input)
        {
            try
            {
                List<MenuRights> list = await _iMenu.GetMenuRightsAsync();
                List<MenuRightsSimple> mlist = new();
                foreach (var item in list)
                {
                    MapperConfiguration config = new(cfg =>
                    {
                        cfg.CreateMap<Permission, SelectRights>()
                        .ForMember(q => q.IsSelect, p => p.MapFrom(w => input.Ids != null && input.Ids.Count > 0 && input.Ids.Any(e => e == w.Id.ToString())));
                    });
                    IMapper mapper = config.CreateMapper();
                    List<SelectRights> Rights = mapper.Map<List<SelectRights>>(item.Rights);
                    mlist.Add(new MenuRightsSimple()
                    {
                        Id = item.Id,
                        ParentId = item.ParentId,
                        Name = item.Name,
                        Code = item.Code,
                        Sort = item.Sort,
                        Rights = Rights,
                        Child = new List<MenuRightsSimple>()
                    });
                }
                List<MenuRightsSimple> topList = new();
                List<MenuRightsSimple> childList = new();
                topList = mlist.FindAll(w => w.ParentId == Guid.Empty);//最高级菜单
                childList = mlist.FindAll(w => w.ParentId != Guid.Empty);//所有子菜单
                foreach (var item in topList)
                {
                    AddChildMenu(item, childList);
                }
                return OutPutMethod<List<MenuRightsSimple>>.Success(topList, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<MenuRightsSimple>>.Failure();
            }
        }

        private void AddChildMenu(MenuRightsSimple parent, List<MenuRightsSimple> childList)
        {
            var childs = childList.FindAll(w => w.ParentId == parent.Id);
            if (childs != null && childs.Count > 0)
            {
                foreach (var item in childs)
                {
                    parent.Child.Add(item);
                    AddChildMenu(item, childList);
                }
            }
        }

        /// <summary>
        /// 新增菜单
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPost("Add")]
        [ModelFilter]
        [Permission("Menu_Add")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Add })]
        public async Task<OutPutModel<object>> AddAsync([FromBody] SysMenuAdd input)
        {
            try
            {
                if (await _iMenu.ExistsByCode(input.Code, null))
                    return OutPutMethod<object>.Failure("该菜单编号已被使用", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<SysMenuAdd, Menu>();
                });
                IMapper mapper = config.CreateMapper();
                Menu data = mapper.Map<SysMenuAdd, Menu>(input);
                data.Id = Guid.NewGuid();
                data.OperaterId = UserInformation.Id;
                data.OperateTime = DateTime.Now;
                if (input.ParentId == null)
                {
                    data.ParentId = Guid.Empty;
                    data.Grade = 1;
                }
                else
                {
                    var parentMenu = await _iMenu.GetByIdAsync(data.ParentId);
                    if (parentMenu != null)
                        data.Grade = parentMenu.Grade + 1;
                    else
                    {
                        data.ParentId = Guid.Empty;
                        data.Grade = 1;
                    }
                }
                return await _iMenu.AddAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }

        /// <summary>
        /// 修改菜单
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPut("Update")]
        [ModelFilter]
        [Permission("Menu_Update")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Update })]
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] SysMenuUpdate input)
        {
            try
            {
                Menu data = await _iMenu.GetByIdAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("菜单信息不存在", null, (int)HttpStatusCode.NotFound);
                if (await _iMenu.ExistsByCode(input.Code, data.Id))
                    return OutPutMethod<object>.Failure("该菜单编号已被使用", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<SysMenuUpdate, Menu>();
                });
                IMapper mapper = config.CreateMapper();
                Menu obj = mapper.Map<SysMenuUpdate, Menu>(input);
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;
                if (input.ParentId == null)
                {
                    obj.ParentId = Guid.Empty;
                    obj.Grade = 1;
                }
                else
                {
                    var parentMenu = await _iMenu.GetByIdAsync(data.ParentId);
                    if (parentMenu != null)
                        obj.Grade = parentMenu.Grade + 1;
                    else
                        obj.Grade = 1;
                }
                return await _iMenu.UpdateAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }

        /// <summary>
        /// 删除菜单
        /// </summary>
        /// <param name="Id">菜单Id</param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Permission("Menu_Delete")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Delete })]
        public async Task<OutPutModel<object>> DeleteAsync(Guid Id)
        {
            try
            {
                Menu data = await _iMenu.GetByIdAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("菜单信息不存在", null, (int)HttpStatusCode.NotFound);
                if (await _iMenu.HasChildAsync(data.Id))
                    return OutPutMethod<object>.Failure("存在下级菜单，不可删除", null, (int)HttpStatusCode.BadRequest);
                return await _iMenu.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}
