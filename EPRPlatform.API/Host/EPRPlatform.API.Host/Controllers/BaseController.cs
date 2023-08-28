
using Microsoft.AspNetCore.Mvc;
using EPRPlatform.API.Models;
using EPRPlatform.API.Dto.OAModels;

namespace EPRPlatform.API.Host.Controllers
{
    /// <summary>
    /// 基础控制器
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : Controller
    {
        /// <summary>
        /// 用户信息
        /// </summary>
        public User UserInformation => ControllerContext == null ? null : ControllerContext.HttpContext.Items["User"] as User;
        /// <summary>
        /// 用户拥有的权限集合
        /// </summary>
        public List<UserRights> UserRights => ControllerContext == null ? null : ControllerContext.HttpContext.Items["UserRights"] as List<UserRights>;
        /// <summary>
        /// 用户菜单
        /// </summary>
        public List<MenuSimple> UserMenus => ControllerContext == null ? null : ControllerContext.HttpContext.Items["UserMenus"] as List<MenuSimple>;
        /// <summary>
        /// 客户端请求IP
        /// </summary>
        public string RequestIP => ControllerContext == null ? null : ControllerContext.HttpContext.Items["RequestIP"] as string;
    }
}
