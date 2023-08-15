
using Microsoft.AspNetCore.Mvc;
using MicroService.Models;
using MicroService.Dto.OAModels;

namespace MicroService.Service.Controllers
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
