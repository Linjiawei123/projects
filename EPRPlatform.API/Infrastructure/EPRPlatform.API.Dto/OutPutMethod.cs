using EPRPlatform.API.Dto.PublicModels;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace EPRPlatform.API.Dto
{
    /// <summary>
    /// 输出方法
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class OutPutMethod<T>
    {
        private static IHttpContextAccessor _httpContextAccessor;

        public static void Initialize(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }

        private static HttpContext CurrentHttpContext => _httpContextAccessor?.HttpContext;
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="data">返回数据</param>
        /// <param name="errType">是否显示提示（1：显示，其余不显示）</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        public static OutPutModel<T> Success(T data = default, int errType = 1, string message = "操作成功")
        {
            OutPutModel<T> outPutModel = new()
            {
                Status = true,
                ErrType = errType,
                Message = string.IsNullOrWhiteSpace(message) ? "操作成功" : message.Trim(),
                Rights = new List<string>(),
                Data = data
            };

            return outPutModel;
        }
        /// <summary>
        /// 返回成功
        /// </summary>
        /// <param name="Rights"></param>
        /// <param name="Data"></param>
        /// <param name="ErrType"></param>
        /// <param name="Message"></param>
        /// <returns></returns>
        public static OutPutModel<T> Success(List<string> Rights, T Data = default, int ErrType = 1, string Message = "操作成功")
        {
            OutPutModel<T> outPutModel = new()
            {
                Status = true,
                ErrType = ErrType,
                Data = Data,
                Message = string.IsNullOrWhiteSpace(Message) ? "操作成功" : Message.Trim(),
                Rights = Rights
            };
            return outPutModel;
        }
        /// <summary>
        /// 返回失败
        /// </summary>
        /// <param name="message">提示信息</param>
        /// <param name="data">返回数据</param>
        /// <param name="statusCode">HTTP状态码</param>
        /// <returns></returns>
        public static OutPutModel<T> Failure(string message = "操作失败", T data = default, int statusCode = (int)HttpStatusCode.InternalServerError)
        {
            if (CurrentHttpContext != null)
                CurrentHttpContext.Response.StatusCode = statusCode;

            OutPutModel<T> outPutModel = new()
            {
                Status = false,
                ErrType = 1,
                Message = string.IsNullOrWhiteSpace(message) ? "操作失败" : message.Trim(),
                Rights = new List<string>(),
                Data = data
            };

            return outPutModel;
        }
    }
}
