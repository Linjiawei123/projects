using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Validation;
using MicroService.Interfaces;
using System.Security.Claims;

namespace MicroService.AuthenticationCenter
{
    public class CustomResourceOwnerPasswordValidator : IResourceOwnerPasswordValidator
    {
        private readonly IUserRepository _userRepository;

        public CustomResourceOwnerPasswordValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            // 根据用户名和密码查询用户
            var user = await _userRepository.GetByAccountAsync(context.UserName);
            if (user != null && user.Password == context.Password)
            {
                // 创建 token
                var claims = new List<Claim>
                {
                    new Claim("userid", user.Id.ToString()),
                    new Claim(JwtClaimTypes.Name,user.Account),
                    new Claim(JwtClaimTypes.NickName,user.Name),
                    new Claim(JwtClaimTypes.Picture,user.Url??"")
                };
                context.Result = new GrantValidationResult(
                 subject: user.Id.ToString(),
                 authenticationMethod: "password",
                 claims: claims);
                return;
            }
            // 验证失败
            context.Result = new GrantValidationResult(TokenRequestErrors.InvalidGrant, "账户或密码有误。");
        }
    }
}
