using Microsoft.AspNetCore.Mvc;
using MicroService.Models;

namespace MicroService.File.Controllers
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
    }
}
