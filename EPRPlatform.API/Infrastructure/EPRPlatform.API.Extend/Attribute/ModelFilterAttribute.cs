using EPRPlatform.API.Dto.PublicModels;
using EPRPlatform.API.Method;
using EPRPlatform.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Net;

namespace EPRPlatform.API.Extend
{
    /// <summary>
    /// 模型过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ModelFilterAttribute : Attribute, IActionFilter
    {
        /// <summary>
        /// 方法调用之后
        /// </summary>
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
        /// <summary>
        /// 方法调用之前
        /// </summary>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            if (context.ModelState.IsValid)
                return;
            else
            {
                List<PublicModel<string, string>> modelErr = PublicMethods.AllModelStateErrors(context.ModelState);
                OutPutModel<List<PublicModel<string, string>>> outPut = new()
                {
                    Status = false,
                    ErrType = 1,
                    Rights = new(),
                    Message = "数据有误，请看详情！",
                    Data = modelErr
                };
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.PreconditionFailed;
                context.Result = new JsonResult(outPut);
            }
        }
    }
}
