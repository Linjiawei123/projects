using Microsoft.AspNetCore.Authentication;
using Newtonsoft.Json;
using MicroService.Models;
using MicroService.Dto.OAModels;
using MicroService.Interfaces;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using MicroService.Dto.PublicModels;

namespace MicroService.Extend
{
    /// <summary>
    /// 授权认证
    /// </summary>
    public class OAAuthenticationHandler : IAuthenticationHandler
    {
        /// <summary>
        /// 方案名称
        /// </summary>
        public const string SchemeName = "OfficePlatformAuth";
        /// <summary>
        /// Scheme
        /// </summary>
        private AuthenticationScheme _scheme;
        private HttpContext _context;
        private readonly ILoginUserRepository _iLoginUser;
        private string AuthMessage = "";
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iLoginUser">用户登录数据接口</param>
        public OAAuthenticationHandler(ILoginUserRepository iLoginUser)
        {
            _iLoginUser = iLoginUser;
        }
        /// <summary>
        /// 初始化认证
        /// </summary>
        /// <param name="scheme"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _scheme = scheme;
            _context = context;
            return Task.CompletedTask;
        }
        /// <summary>
        /// 生成认证票据
        /// </summary>
        /// <returns></returns>
        public Task<AuthenticateResult> AuthenticateAsync()
        {
            var token = _context.Request.Headers["Authorization"].ToString();
            if (string.IsNullOrWhiteSpace(token))
            {
                AuthMessage = "未登陆";
                return Task.FromResult(AuthenticateResult.Fail(AuthMessage));
            }

            if (!token.ToLower().StartsWith("bearer "))
            {
                AuthMessage = "未登陆";
                return Task.FromResult(AuthenticateResult.Fail(AuthMessage));
            }

            string[] strs = token.Split(' ');
            if (strs.Length != 2)
            {
                AuthMessage = "未登陆";
                return Task.FromResult(AuthenticateResult.Fail(AuthMessage));
            }
            token = strs[1];

            var userlogin = _iLoginUser.GetUserLoginAsync(token).Result;
            if (userlogin == null)
            {
                AuthMessage = "未登陆";
                return Task.FromResult(AuthenticateResult.Fail(AuthMessage));
            }

            if (userlogin.LoginTime.AddSeconds(userlogin.Expires_in) < DateTime.Now)
            {
                AuthMessage = "登陆过期";
                return Task.FromResult(AuthenticateResult.Fail(AuthMessage));
            }
            _ = new List<MenuSimple>();
            var data = _iLoginUser.GetUserAsync(userlogin.UserId).Result;
            if (data != null)
            {
                User user = data;
                List<UserRights> userRights = _iLoginUser.GetUserRightsAsync(data.Id).Result;
                var list = _iLoginUser.GetUserMenusAsync(data.Id).Result;
                List<MenuSimple> userMenus = MenusMethod(list);
                _context.Items.Add("User", user);
                _context.Items.Add("UserRights", userRights);
                _context.Items.Add("UserMenus", userMenus);
                var ticket = GetAuthTicket(userlogin.UserId.ToString());
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }

            AuthMessage = "未登陆";
            return Task.FromResult(AuthenticateResult.Fail(AuthMessage));
        }
        /// <summary>
        /// 未登录时的处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            OutPutModel<object> output = new()
            {
                Status = false,
                ErrType = 1,
                Message = AuthMessage,
                Rights = new List<string>(),
                Data = null
            };
            _context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            _context.Response.ContentType = "application/json; charset=utf-8";
            _context.Response.WriteAsync(JsonConvert.SerializeObject(output)).Wait();
            return Task.CompletedTask;
        }
        /// <summary>
        /// 权限不足时处理
        /// </summary>
        /// <param name="properties"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Task ForbidAsync(AuthenticationProperties properties)
        {
            OutPutModel<object> output = new()
            {
                Status = false,
                ErrType = 1,
                Message = "登录验证失败",
                Rights = new List<string>(),
                Data = null
            };
            _context.Response.ContentType = "application/json; charset=utf-8";
            _context.Response.WriteAsync(JsonConvert.SerializeObject(output)).Wait();
            _context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            return Task.CompletedTask;
        }

        /// <summary>
        /// 生成认证票据
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        private AuthenticationTicket GetAuthTicket(string UserId)
        {
            var claimsIdentity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, UserId)
            }, SchemeName);
            var principal = new ClaimsPrincipal(claimsIdentity);
            return new AuthenticationTicket(principal, _scheme.Name);
        }


        private List<MenuSimple> MenusMethod(List<Menu> list)
        {
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
            List<MenuSimple> topList = mlist.FindAll(w => w.ParentId == Guid.Empty);//最高级菜单
            List<MenuSimple> childList = mlist.FindAll(w => w.ParentId != Guid.Empty);//所有子菜单
            foreach (var item in topList)
            {
                AddChildMenu(item, childList);
            }
            return topList;
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
    }
}
