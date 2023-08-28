using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Extend
{
    public class CustomAuthenticationHandler : IAuthenticationHandler
    {
        private HttpContext _context;

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _context = context;
            return Task.CompletedTask;
        }

        public Task<AuthenticateResult> AuthenticateAsync()
        {
            // 执行自定义的身份认证逻辑，并将结果封装为AuthenticateResult对象返回

            // 如果认证成功，则创建一个ClaimsPrincipal对象，并使用成功的认证票据初始化
            var identity = new ClaimsIdentity("CustomAuthenticationScheme");
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, "CustomAuthenticationScheme");

            // 如果认证失败，则返回一个AuthenticateResult对象，设置Failed结果
            //if (!AuthenticateSuccess())
            //{
            //    return Task.FromResult(AuthenticateResult.Fail("Authentication failed"));
            //}

            return Task.FromResult(AuthenticateResult.Success(ticket));
        }

        public Task ChallengeAsync(AuthenticationProperties properties)
        {
            // 处理身份认证挑战的逻辑

            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties properties)
        {
            // 处理身份认证禁止的逻辑

            return Task.CompletedTask;
        }
    }

}
