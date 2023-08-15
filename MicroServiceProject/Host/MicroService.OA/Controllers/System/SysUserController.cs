using Microsoft.AspNetCore.Mvc;
using MicroService.Interfaces;
using System.Net;
using  MicroService.Dto.OAModels;
using AutoMapper;
using MicroService.Models;
using MicroService.Method;
using MicroService.Dto.PublicModels;
using MicroService.Extend;
using Microsoft.AspNetCore.Authorization;

namespace MicroService.OA.Controllers
{
    /// <summary>
    /// 用户管理
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FieldFilter]
    [Authorize]
    public class SysUserController : BaseController
    {
        private const int _module = 10003;
        private readonly IErrorRepository _iError;
        private readonly IUserRepository _iUser;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError">错误数据接口</param>
        /// <param name="iUser">用户数据接口</param>
        public SysUserController(IErrorRepository iError, IUserRepository iUser)
        {
            try
            {
                _iError = iError;
                _iUser = iUser;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 获取分页
        /// </summary>
        /// <param name="Account">账号</param>
        /// <param name="Name">昵称</param>
        /// <param name="pageSize">每页记录数</param>
        /// <param name="pageIndex">页码</param>
        /// <returns></returns>
        [HttpPost]
        [Route("{pageSize}/{pageIndex}")]
        [Permission("User_View")]
        public async Task<OutPutModel<PageModel<List<UserSimple>>>> GetPageAsync([FromForm] string Account, [FromForm] string Name, short pageSize, int pageIndex)
        {
            try
            {
                PageModel<List<User>> list = await _iUser.GetPageAsync(Account, Name, pageSize, pageIndex);
                PageModel<List<UserSimple>> page = new()
                {
                    RecordCount = list.RecordCount,
                    PageCount = list.PageCount,
                    PageData = new List<UserSimple>()
                };
                foreach (var item in list.PageData)
                {
                    page.PageData.Add(new UserSimple()
                    {
                        Id = item.Id,
                        Account = item.Account,
                        Name = item.Name,
                        Status = item.Status,
                        Url = item.Url,
                        UserType = item.UserType,
                        Rights = item.Rights != null && item.Rights.Count > 0 ? item.Rights.Select(w => w.PermissionId).ToList() : new List<Guid>()
                    });
                }
                return OutPutMethod<PageModel<List<UserSimple>>>.Success(page, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<PageModel<List<UserSimple>>>.Failure();
            }
        }
        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="Id">数据id</param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [Permission("User_View")]
        public async Task<OutPutModel<User>> GetAsync(Guid Id)
        {
            try
            {
                User data = await _iUser.GetByIdAsync(Id);
                if (data == null)
                    return OutPutMethod<User>.Failure("用户信息不存在", null, (int)HttpStatusCode.NotFound);
                return OutPutMethod<User>.Success(data, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<User>.Failure();
            }
        }
        /// <summary>
        /// 新增用户
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ModelFilter]
        [Permission("User_Add")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Add })]
        public async Task<OutPutModel<object>> AddAsync([FromBody] UserAdd input)
        {
            try
            {
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<UserAdd, User>();
                });
                IMapper mapper = config.CreateMapper();
                User data = mapper.Map<User>(input);
                data.Id = Guid.NewGuid();
                data.ErrTimes = 0;
                data.OperaterId = UserInformation.Id;
                data.OperateTime = DateTime.Now;
                List<UserAndPermission> rights = new();
                if (input.RightsId != null && input.RightsId.Count > 0)
                {
                    foreach (var item in input.RightsId)
                    {
                        rights.Add(new UserAndPermission()
                        {
                            Id = Guid.NewGuid(),
                            UserId = data.Id,
                            PermissionId = item
                        });
                    }
                }
                return await _iUser.AddAsync(data, rights) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure(); ;
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
        /// <summary>
        /// 修改
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ModelFilter]
        [Permission("User_Update")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Update })]
        public async Task<OutPutModel<object>> UpdateAsync([FromBody] UserUpdate input)
        {
            try
            {
                User data = await _iUser.GetByIdAsync(input.Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("用户信息不存在", null, (int)HttpStatusCode.NotFound);
                if (input.Password.IsNullOrEmpty())
                    input.Password = data.Password;
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<UserUpdate, User>();
                });
                IMapper mapper = config.CreateMapper();
                User obj = mapper.Map<User>(input);
                obj.OperaterId = UserInformation.Id;
                obj.OperateTime = DateTime.Now;

                List<UserAndPermission> addRights = new();
                List<UserAndPermission> delRights = new();
                List<Guid> old = data.Rights != null && data.Rights.Count > 0 ? data.Rights.Select(w => w.PermissionId).ToList() : new();
                ListEdit<Guid> rightsId = PublicMethods.GetListEdit<Guid>(old, input.RightsId);
                if (rightsId.ListAdd.Count > 0)
                {
                    foreach (var item in input.RightsId)
                    {
                        addRights.Add(new UserAndPermission()
                        {
                            Id = Guid.NewGuid(),
                            UserId = obj.Id,
                            PermissionId = item
                        });
                    }
                }
                if (data.Rights != null && data.Rights.Count > 0 && rightsId.ListDelete.Count > 0)
                    delRights = data.Rights.FindAll(w => rightsId.ListDelete.Contains(w.PermissionId)).ToList();
                return await _iUser.UpdateAsync(obj, addRights, delRights) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id}")]
        [ModelFilter]
        [Permission("User_Delete")]
        [TypeFilter(typeof(OperateLogAttribute), Arguments = new object[] { _module, OperateEnum.Delete })]
        public async Task<OutPutModel<object>> DeleteAsync(Guid Id)
        {
            try
            {
                User data = await _iUser.GetByIdAsync(Id);
                if (data == null)
                    return OutPutMethod<object>.Failure("用户信息不存在", null, (int)HttpStatusCode.NotFound);
                if (!await _iUser.DeleteAsync(data))
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
