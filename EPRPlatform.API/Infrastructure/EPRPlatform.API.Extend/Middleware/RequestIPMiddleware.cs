using EPRPlatform.API.Dto.OAModels;
using EPRPlatform.API.Interfaces;
using EPRPlatform.API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Extend.Middleware
{
    public class RequestIPMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestIPMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // 获取客户端的真实IP地址
            string clientIP = context.Connection.RemoteIpAddress?.ToString();

            // 将客户端IP地址添加到请求头
            context.Request.Headers.Add("X-Forwarded-For", clientIP);
            await _next(context);
        }
    }
}
