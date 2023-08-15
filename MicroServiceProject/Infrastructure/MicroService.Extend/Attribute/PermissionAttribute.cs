using MicroService.Method;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;
using MicroService.Models;
using MicroService.Dto.PublicModels;

namespace MicroService.Extend
{
    /// <summary>
    /// 权限认证
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PermissionAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 权限数组
        /// </summary>
        private string[] _permission;
        /// <summary>
        /// 构造函数
        /// </summary>
        public PermissionAttribute(string permission)
        {
            _permission = new string[] { permission };
        }
        /// <summary>
        /// 构造函数
        /// </summary>
        public PermissionAttribute(string[] permission)
        {
            _permission = permission;
        }
        /// <summary>
        /// 方法实现之后
        /// </summary>
        public void OnActionExecuted(ActionExecutedContext context)
        {

        }
        /// <summary>
        /// 方法实现之前
        /// </summary>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            bool hasRight = false;
            if (context.HttpContext.Items["UserRights"] is List<UserRights> userRights && userRights.Count > 0)
            {
                foreach (var item in userRights)
                {
                    if (_permission.Contains(item.Code))
                    {
                        hasRight = true;
                        break;
                    }
                }
            }
            if (hasRight)
                return;
            else
            {
                OutPutModel<object> output = new()
                {
                    Status = false,
                    ErrType = 1,
                    Message = "您无此操作权限",
                    Rights = null,
                    Data = null
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.Forbidden;
                context.Result = new JsonResult(output);
            }
        }
    }
}
