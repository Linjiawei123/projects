
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EPRPlatform.API.Dto.OAModels
{
    /// <summary>
    /// 登录
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0}格式有误")]
        public string Account { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "{0}格式有误")]
        public string Password { get; set; }
    }
    /// <summary>
    /// 登录成功返回model
    /// </summary>
    public class TokenResult
    {
        /// <summary>
        /// token
        /// </summary>
        public string access_token { get; set; }
        /// <summary>
        /// 刷新token
        /// </summary>
        public string refresh_token { get; set; }
        /// <summary>
        /// 过期时间(单位秒)
        /// </summary>
        public int expires_in { get; set; }
    }
    /// <summary>
    /// 登录用户信息
    /// </summary>
    public class LoginUserInfo
    {
        /// <summary>
        /// 用户id
        /// </summary>
        public string Id { get; set; }
        /// <summary>
        /// 用户昵称
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 用户头像
        /// </summary>
        public string Url { get; set; }
        /// <summary>
        /// 用户权限
        /// </summary>
        public List<string> Rights { get; set; }
        /// <summary>
        /// 用户菜单
        /// </summary>
        public List<MenuSimple> Menus { get; set; }
    }
}
