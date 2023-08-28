using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EPRPlatform.API.Models;
using EPRPlatform.API.Dto.OAModels;
using EPRPlatform.API.Method;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using EPRPlatform.API.Dto;

namespace EPRPlatform.API.Host.Controllers
{
    /// <summary>
    /// 登录用户信息
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FieldFilter]
    [Authorize]
    public class LoginUserController : BaseController
    {
        private readonly IErrorRepository _iError;
        private readonly IUserRepository _iUser;
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iError">错误数据</param>
        /// <param name="iUser">用户</param>
        public LoginUserController(IErrorRepository iError, IUserRepository iUser)
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
        /// 获取登录用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<OutPutModel<LoginUserInfo>> GetUserInfoAsync()
        {
            try
            {
                LoginUserInfo loginUserInfo = new()
                {
                    Id = UserInformation.Id.ToString(),
                    UserName = UserInformation.Name,
                    Account = UserInformation.Account,
                    Url = UserInformation.Url.IsNullOrWhiteSpace() ? "" : UserInformation.Url,
                    Rights = UserRights.Count > 0 ? UserRights.Select(w => w.Code).ToList() : new List<string>(),
                    Menus = UserMenus.Count > 0 ? UserMenus : new List<MenuSimple>()
                };

                return OutPutMethod<LoginUserInfo>.Success(loginUserInfo, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<LoginUserInfo>.Failure();
            }
        }
        /// <summary>
        /// 修改个人用户信息
        /// </summary>
        /// <param name="input">参数</param>
        /// <returns></returns>
        [HttpPut]
        public async Task<OutPutModel<object>> UpdateUserAsync([FromBody] UserInfoUpdate input)
        {
            try
            {
                MapperConfiguration config = new(cfg =>
                {
                    cfg.CreateMap<UserInfoUpdate, User>()
                    .ForMember(q => q.Password, p => p.MapFrom(w => w.Password ?? UserInformation.Password));
                });
                IMapper mapper = config.CreateMapper();
                User obj = mapper.Map<User>(input);
                obj.Id = UserInformation.Id;
                return await _iUser.UpdateUserInfoAsync(obj) ? OutPutMethod<object>.Success() : OutPutMethod<object>.Failure();
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<object>.Failure();
            }
        }
    }
}
