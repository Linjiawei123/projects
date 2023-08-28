using EPRPlatform.API.Models;
using EPRPlatform.API.Dto.OAModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Method;
using Microsoft.Extensions.Options;
using EPRPlatform.API.Method.Check;
using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Extend;
using Azure;
using Newtonsoft.Json;
using EPRPlatform.API.Dto;

namespace EPRPlatform.API.Host
{
    /// <summary>
    /// 登录
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [FieldFilter]
    public class SysLoginController : ControllerBase
    {
        private readonly LoginConfig _options;
        private readonly IErrorRepository _iError;
        private readonly IHttpAPIInvoker _iHttpAPIInvoker;
        private readonly ILoginUserRepository _iLoginUser;
        /// <summary>
        /// 构造函数
        /// </summary>
        public SysLoginController(IErrorRepository iError, ILoginUserRepository iLoginUser, IHttpAPIInvoker iHttpAPIInvoker, IOptions<LoginConfig> options)
        {
            try
            {
                _options = options.Value;
                _iError = iError;
                _iLoginUser = iLoginUser;
                _iHttpAPIInvoker = iHttpAPIInvoker;
            }
            catch (Exception ex)
            {
                _iError.AddErrorAsync(ex).Wait();
            }
        }
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="input">实体</param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        public async Task<OutPutModel<TokenResult>> LoginAsync([FromBody] LoginDto input)
        {
            try
            {
                DateTime now = DateTime.Now;
                int Times = _options.MaxErrTimes;
                int Minutes = _options.LockTime;
                string loginIP = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault() ?? HttpContext.Connection.RemoteIpAddress?.ToString();
                if (_options.LimitOfIP)
                {
                    if (!ValidHepler.EffectiveIP(loginIP, _options.IPRange))
                        return OutPutMethod<TokenResult>.Failure($"当前IP：{loginIP} 不在可登录范围内！", null, (int)HttpStatusCode.BadRequest);
                }

                User user = await _iLoginUser.GetByAccountAsync(input.Account);
                if (user == null)
                    return OutPutMethod<TokenResult>.Failure("账号不存在！", null, (int)HttpStatusCode.BadRequest);
                if (user.Status == 2)
                    return OutPutMethod<TokenResult>.Failure("当前账号已被禁用，请联系管理员！", null, (int)HttpStatusCode.BadRequest);
                if (user.Status == 3 && user.FirstErrTime.Value.AddMinutes(Minutes) > now)
                {
                    DateTime recoveryTime = user.FirstErrTime.Value.AddMinutes(Minutes);
                    return OutPutMethod<TokenResult>.Failure($"当前账号已被锁定，请联系管理员或在 {recoveryTime:yyyy-MM-dd HH:mm:ss} 时间之后重新尝试登录！", null, (int)HttpStatusCode.BadRequest);
                }
                if (user.Password != input.Password)
                {
                    if (user.ErrTimes < Times)
                    {
                        if (user.ErrTimes == 0)
                            user.FirstErrTime = now;
                        user.ErrTimes++;
                        await _iLoginUser.LoginErrAsync(user);
                        return OutPutMethod<TokenResult>.Failure($"密码错误，还剩{Times - user.ErrTimes}次机会", null, (int)HttpStatusCode.BadRequest);
                    }
                    else
                    {
                        user.Status = 3;
                        DateTime recoveryTime = user.FirstErrTime.Value.AddMinutes(Minutes);
                        await _iLoginUser.LoginErrAsync(user);
                        return OutPutMethod<TokenResult>.Failure($"当前账号因重复尝试 {Times} 次密码后被锁定，请联系管理员或在 {recoveryTime:yyyy-MM-dd HH:mm:ss} 时间之后重新尝试登录！", null, (int)HttpStatusCode.BadRequest);
                    }
                }
                else
                {
                    user.Status = 1;
                    user.ErrTimes = 0;
                    user.FirstErrTime = null;
                }

                var formData = new Dictionary<string, string>
                {
                    { "grant_type", "password" },
                    { "client_id", "OAClient" },
                    { "client_secret", "OASecret" },
                    { "UserName", input.Account },
                    { "Password", input.Password },
                    { "Scope", "OAApi offline_access openid profile" }
                };
                var response = await _iHttpAPIInvoker.PostFormUrlEncodedAsync<IdentityResult>(_options.LoginUrl, formData);
                if (!response.IsSuccess)
                    return OutPutMethod<TokenResult>.Failure(response.ErrorMessage, null, (int)HttpStatusCode.BadRequest);

                TokenResult res = new()
                {
                    access_token = response.Data.access_token,
                    expires_in = response.Data.expires_in,
                    refresh_token = response.Data.refresh_token
                };

                UserLogin userLogin = await _iLoginUser.GetAsync(user.Id);
                UserLoginLog log = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    LoginTime = DateTime.Now,
                    LoginIP = loginIP
                };
                if (userLogin == null)
                {
                    UserLogin data = new()
                    {
                        Id = Guid.NewGuid(),
                        UserId = user.Id,
                        Access_token = res.access_token,
                        Expires_in = res.expires_in,
                        Refresh_token = res.refresh_token,
                        LoginTime = DateTime.Now
                    };
                    if (!await _iLoginUser.AddAsync(data, log))
                        return OutPutMethod<TokenResult>.Failure("登录失败");
                }
                else
                {
                    userLogin.Access_token = res.access_token;
                    userLogin.Expires_in = res.expires_in;
                    userLogin.Refresh_token = res.refresh_token;
                    userLogin.LoginTime = DateTime.Now;
                    if (!await _iLoginUser.UpdateAsync(userLogin, user, log))
                        return OutPutMethod<TokenResult>.Failure("登录失败");
                }
                return OutPutMethod<TokenResult>.Success(res, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<TokenResult>.Failure();
            }
        }
        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="refresh_token">刷新token</param>
        /// <returns></returns>
        [HttpPost("RefreshToken")]
        public async Task<OutPutModel<TokenResult>> RefreshTokenAsync([FromForm] string refresh_token)
        {
            try
            {
                UserLogin userLogin = await _iLoginUser.GetRefreshTokenAsync(refresh_token);
                if (userLogin == null)
                    return OutPutMethod<TokenResult>.Failure("刷新token失败", null, (int)HttpStatusCode.BadRequest);
                DateTime now = DateTime.Now;
                var formData = new Dictionary<string, string>
                {
                    { "grant_type", "refresh_token" },
                    { "client_id", "OAClient" },
                    { "client_secret", "OASecret" },
                    { "refresh_token", refresh_token },
                    { "Scope", "OAApi offline_access openid profile" }
                };
                var response = await _iHttpAPIInvoker.PostFormUrlEncodedAsync<IdentityResult>(_options.LoginUrl, formData);
                if (!response.IsSuccess)
                    return OutPutMethod<TokenResult>.Failure(response.ErrorMessage, null, (int)HttpStatusCode.BadRequest);

                TokenResult res = new()
                {
                    access_token = response.Data.access_token,
                    expires_in = response.Data.expires_in,
                    refresh_token = response.Data.refresh_token
                };

                userLogin.Access_token = res.access_token;
                userLogin.Expires_in = res.expires_in;
                userLogin.Refresh_token = res.refresh_token;
                if (!await _iLoginUser.UpdateAsync(userLogin, null, null))
                    return OutPutMethod<TokenResult>.Failure("刷新token失败");
                return OutPutMethod<TokenResult>.Success(res, 0);
            }
            catch (Exception ex)
            {
                await _iError.AddErrorAsync(ex);
                return OutPutMethod<TokenResult>.Failure();
            }
        }
    }
}
