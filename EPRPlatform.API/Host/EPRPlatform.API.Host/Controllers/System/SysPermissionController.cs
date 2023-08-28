using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using EPRPlatform.API.Models;
using  EPRPlatform.API.Dto.OAModels;
using EPRPlatform.API.Interfaces;
using System.Net;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using Microsoft.AspNetCore.Authorization;
using EPRPlatform.API.Dto;

namespace EPRPlatform.API.Host.Controllers.System
{
    /// <summary>
    /// 权限管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FieldFilter]
    [Authorize]
    public class SysPermissionController : BaseController
    {
        private const int _module = 10002;
        private readonly IErrorRepository _iError;
        private readonly IPermissionRepository _iPermission;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError">错误数据接口</param>
        /// <param name="iPermission">权限数据接口</param>
        public SysPermissionController(IErrorRepository iError, IPermissionRepository iPermission)
        {
            try
            {
                _iError = iError;
                _iPermission = iPermission;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }

        /// <summary>
        /// 获取菜单权限列表
        /// </summary>
        /// <param name="menuId">菜单Id</param>
        /// <returns></returns>
        [HttpGet]
        [Route("{menuId}")]
        [Permission("Permission_View")]
        public async Task<OutPutModel<List<SysPermissionSimple>>> GetMenuPermissionAsync(Guid menuId)
        {
            try
            {
                List<Permission> list = await _iPermission.GetByMenuIdAsync(menuId);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<Permission, SysPermissionSimple>();
                });
                IMapper mapper = config.CreateMapper();
                List<SysPermissionSimple> data = mapper.Map<List<Permission>, List<SysPermissionSimple>>(list);
                return OutPutMethod<List<SysPermissionSimple>>.Success(data);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<List<SysPermissionSimple>>.Failure();
            }
        }

        /// <summary>
        /// 新增菜单权限
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        [Permission("Permission_Add")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Add })]
        public async Task<OutPutModel<object>> AddAsync([FromBody] SysPermissionAdd input)
        {
            try
            {
                if (await _iPermission.IsHasByCode(input.Code))
                    return OutPutMethod<object>.Failure("权限代码已被使用", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<SysPermissionAdd, Permission>();
                });
                IMapper mapper = config.CreateMapper();
                Permission data = mapper.Map<SysPermissionAdd, Permission>(input);
                data.Id = Guid.NewGuid();
                data.OperaterId = UserInformation.Id;
                data.OperateTime = DateTime.Now;
                if (!await _iPermission.AddAsync(data))
                    return OutPutMethod<object>.Failure();
                return OutPutMethod<object>.Success();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }

        /// <summary>
        /// 修改菜单权限
        /// </summary>
        /// <param name="input">输入实体</param>
        /// <returns></returns>
        [HttpPut]
        [ModelFilter]
        [Permission("Permission_Update")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Update })]
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] SysPermissionUpdate input)
        {
            try
            {
                var data = await _iPermission.GetByIdAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("权限不存在", null, (int)HttpStatusCode.NotFound);
                if (await _iPermission.IsHasByCode(input.Code) && input.Code != data.Code)
                    return OutPutMethod<object>.Failure("权限代码已被使用", null, (int)HttpStatusCode.BadRequest);
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<SysPermissionUpdate, Permission>();
                });
                IMapper mapper = config.CreateMapper();
                Permission obj = mapper.Map<SysPermissionUpdate, Permission>(input);
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;
                if (!await _iPermission.UpdateAsync(obj))
                    return OutPutMethod<object>.Failure();
                return OutPutMethod<object>.Success();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }

        /// <summary>
        /// 删除权限
        /// </summary>
        /// <param name="Id">权限Id</param>
        /// <returns></returns>
        [HttpDelete]
        [Permission("Permission_Delete")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Delete })]
        public async Task<OutPutModel<object>> DeleteAsync(Guid Id)
        {
            try
            {
                var data = await _iPermission.GetByIdAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("权限不存在", null, (int)HttpStatusCode.NotFound);
                if (!await _iPermission.DeleteAsync(data))
                    return OutPutMethod<object>.Failure();
                return OutPutMethod<object>.Success();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}
