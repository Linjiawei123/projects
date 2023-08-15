using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using MicroService.Models;
using System.Diagnostics;
using MicroService.Dto.OAModels;

namespace MicroService.Extend
{
    /// <summary>
    /// 操作日志记录
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class OperateLogAttribute : Attribute, IActionFilter
    {
        private readonly Stopwatch _timer;
        private readonly int _module;
        private readonly int _operate;
        private readonly IRedisInvoker _iRedisInvoker;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="iRedisInvoker"></param>
        /// <param name="module">操作模块</param>
        /// <param name="operate">操作类型</param>
        public OperateLogAttribute(IRedisInvoker iRedisInvoker,int module, OperateEnum operate)
        {
            _timer = new Stopwatch();
            _module = module;
            _operate = (int)operate;
            _iRedisInvoker = iRedisInvoker;
        }
        /// <summary>
        /// 方法使用前
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _timer.Start();
        }
        /// <summary>
        /// 方法使用后
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            _timer.Stop();
            OperateLog logEntry = new()
            {
                Module = _module,
                Operate = _operate,
                UserId = context.HttpContext.Items["User"] is User user ? user.Id : Guid.Empty,
                OperateTime = DateTime.Now,
                Method = context.HttpContext.Request.Method,
                Path = context.HttpContext.Request.Path,
                QueryString = context.HttpContext.Request.QueryString.Value,
                RequestBody = GetRequestBody(context.HttpContext.Request.Body),
                StatusCode = context.HttpContext.Response.StatusCode,
                ResponseBody = GetResponseBody(context.Result),
                DurationMs = _timer.ElapsedMilliseconds
            };
            string key = "operateLog";
            var json = JsonConvert.SerializeObject(logEntry);
            _iRedisInvoker.ListRightPush(key, json);//单独做定时存数据库
        }

        private static string GetRequestBody(Stream body)
        {
            body.Position = 0;
            using var reader = new StreamReader(body);
            return reader.ReadToEndAsync().Result;
        }

        private static string GetResponseBody(object result)
        {
            if (result == null)
                return string.Empty;
            if (result is string)
                return result as string;
            if (result is ObjectResult)
            {
                var objectResult = result as ObjectResult;
                return JsonConvert.SerializeObject(objectResult.Value);
            }
            return JsonConvert.SerializeObject(result);
        }
    }
}
