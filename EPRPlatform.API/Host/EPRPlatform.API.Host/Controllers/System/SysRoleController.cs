using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EPRPlatform.API.Models;
using  EPRPlatform.API.Dto.OAModels;
using EPRPlatform.API.Method;
using EPRPlatform.API.Interfaces;
using System.Net;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using Microsoft.AspNetCore.Authorization;
using EPRPlatform.API.Dto;

namespace EPRPlatform.API.Host.Controllers.System
{
    /// <summary>
    /// 角色管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FieldFilter]
    [Authorize]
    public class SysRoleController : BaseController
    {
        private const int _module = 10004;
        private readonly IErrorRepository _iError;
        private readonly IRoleRepository _iRole;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError">错误数据接口</param>
        /// <param name="iRole">角色数据接口</param>
        public SysRoleController(IErrorRepository iError, IRoleRepository iRole)
        {
            try
            {
                _iError = iError;
                _iRole = iRole;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 角色分页
        /// </summary>
        /// <param name="Keyword">关键词</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost("{pageSize}/{pageIndex}")]
        [Permission("Role_View")]
        public async Task<OutPutModel<PageModel<List<RoleSimple>>>> GetPageAsync([FromForm] string Keyword, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<Role>> list = await _iRole.GetRolePageAsync(Keyword, pageSize, pageIndex);
                PageModel<List<RoleSimple>> page = new()
                {
                    RecordCount = list.RecordCount,
                    PageCount = list.PageCount,
                    PageData = new List<RoleSimple>()
                };
                foreach (var item in list.PageData)
                {
                    page.PageData.Add(new RoleSimple()
                    {
                        Id = item.Id,
                        Code = item.Code,
                        Name = item.Name,
                        Status = item.Status,
                        Remark = item.Remark,
                        Rights = item.Rights != null && item.Rights.Count > 0 ? item.Rights.Select(w => w.PermissionId).ToList() : new List<Guid>()
                    });
                }
                return OutPutMethod<PageModel<List<RoleSimple>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<RoleSimple>>>.Failure();
            }
        }
        /// <summary>
        /// 新增角色
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        [Permission("Role_Add")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Add })]
        public async Task<OutPutModel<object>> AddAsync([FromBody] RoleAdd input)
        {
            try
            {

                if (await _iRole.AnyCode(input.Code.Trim()))
                    return OutPutMethod<object>.Failure("编号已被使用", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<RoleAdd, Role>();
                });
                IMapper mapper = config.CreateMapper();
                Role obj = mapper.Map<Role>(input);
                obj.Id = Guid.NewGuid();
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;
                List<RoleAndPermission> rights = new();
                if (input.RightsId != null && input.RightsId.Count > 0)
                {
                    foreach (var item in input.RightsId)
                    {
                        rights.Add(new RoleAndPermission()
                        {
                            Id = Guid.NewGuid(),
                            RoleId = obj.Id,
                            PermissionId = item
                        });
                    }
                }
                return await _iRole.AddAsync(obj, rights) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
        /// <summary>
        /// 修改角色
        /// </summary>
        /// <param name="input">输入</param>
        /// <returns></returns>
        [HttpPut]
        [ModelFilter]
        [Permission("Role_Update")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Update })]
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] RoleUpdate input)
        {
            try
            {
                Role data = await _iRole.Get(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("角色信息不存在", null, (int)HttpStatusCode.NotFound);
                if (input.Code != data.Code && await _iRole.AnyCode(input.Code.Trim()))
                    return OutPutMethod<object>.Failure("编号已被使用", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<RoleUpdate, Role>();
                });
                IMapper mapper = config.CreateMapper();
                Role obj = mapper.Map<Role>(input);
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;
                List<RoleAndPermission> addRights = new();
                List<RoleAndPermission> delRights = new();
                List<Guid> old = data.Rights != null && data.Rights.Count > 0 ? data.Rights.Select(w => w.PermissionId).ToList() : new();
                ListEdit<Guid> rightsId = PublicMethods.GetListEdit<Guid>(old, input.RightsId);
                if (rightsId.ListAdd.Count > 0)
                {
                    foreach (var item in input.RightsId)
                    {
                        addRights.Add(new RoleAndPermission()
                        {
                            Id = Guid.NewGuid(),
                            RoleId = obj.Id,
                            PermissionId = item
                        });
                    }
                }
                if (data.Rights != null && data.Rights.Count > 0 && rightsId.ListDelete.Count > 0)
                    delRights = data.Rights.FindAll(w => rightsId.ListDelete.Contains(w.PermissionId)).ToList();
                return await _iRole.UpdateAsync(obj, addRights, delRights) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
        /// <summary>
        /// 删除角色
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [Permission("Role_Delete")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Delete })]
        public async Task<OutPutModel<object>> UpdateAsync(Guid Id)
        {
            try
            {
                Role data = await _iRole.Get(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("角色信息不存在", null, (int)HttpStatusCode.NotFound);
                return await _iRole.DeleteAsync(data) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <returns></returns>
        [HttpPost("UserList")]
        [Permission("Role_User")]
        public async Task<OutPutModel<List<UserData>>> GetUsersAsync()
        {
            try
            {
                List<User> list = await _iRole.GetUsersAsync();
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<User, UserData>()
                    .ForMember(q => q.Name, p => p.MapFrom(w => w.Name + $"({w.Account})"));
                });
                IMapper mapper = config.CreateMapper();
                List<UserData> data = mapper.Map<List<UserData>>(list);
                return OutPutMethod<List<UserData>>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<UserData>>.Failure();
            }
        }
        /// <summary>
        /// 获取角色成员
        /// </summary>
        /// <param name="RoleId">角色id</param>
        /// <returns></returns>
        [HttpPost("RoleUser/{RoleId}")]
        [Permission("Role_User")]
        public async Task<OutPutModel<List<Guid>>> GetRoleUserAsync(Guid RoleId)
        {
            try
            {
                List<RoleUserInfo> list = await _iRole.GetRoleAndUserAsync(RoleId);
                List<Guid> roleuser = new();
                roleuser = list.Select(x => x.UserId).ToList();
                return OutPutMethod<List<Guid>>.Success(roleuser, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<Guid>>.Failure();
            }
        }
        /// <summary>
        /// 角色成员管理
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost("RoleUser")]
        [ModelFilter]
        [Permission("Role_User")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.RoleUser })]
        public async Task<OutPutModel<object>> RoleUserAsync([FromBody] RoleUserManage input)
        {
            try
            {
                Role data = await _iRole.Get(input.RoleId);
                if (data == null)
                    return OutPutMethod<object>.Failure("角色信息不存在", null, (int)HttpStatusCode.NotFound);
                List<RoleAndUser> addUser = new();
                List<RoleAndUser> delUser = new();
                List<RoleAndUser> list = await _iRole.GetRoleUsersAsync(input.RoleId);
                List<Guid> old = list.Count > 0 ? list.Select(w => w.UserId).ToList() : new();
                ListEdit<Guid> userId = PublicMethods.GetListEdit<Guid>(old, input.UserId);
                if (userId.ListAdd.Count > 0)
                {
                    foreach (var item in input.UserId)
                    {
                        addUser.Add(new RoleAndUser()
                        {
                            Id = Guid.NewGuid(),
                            RoleId = data.Id,
                            UserId = item
                        });
                    }
                }
                if (list.Count > 0 && userId.ListDelete.Count > 0)
                    delUser = list.FindAll(w => userId.ListDelete.Contains(w.UserId)).ToList();
                if (addUser.Count == 0 && delUser.Count == 0)
                    return OutPutMethod<object>.Success();
                return await _iRole.RoleUserAsync(addUser, delUser) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}
