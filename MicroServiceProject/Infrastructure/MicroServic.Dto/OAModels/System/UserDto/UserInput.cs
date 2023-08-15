using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 新增用户
    /// </summary>
    public class UserAdd
    {
        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Account { get; set; }
        /// <summary>
        /// 密码（经32位MD5加密）
        /// </summary>
        [DisplayName("密码")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(32, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [DisplayName("昵称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
        /// <summary>
        /// 账号状态 1.正常 2.禁用 3.锁定
        /// </summary>
        [DisplayName("用户类型")]
        [Required(ErrorMessage = "请选择{0}")]
        public int Status { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public List<Guid> RightsId { get; set; }
    }
    /// <summary>
    /// 修改用户
    /// </summary>
    public class UserUpdate
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [DisplayName("用户ID")]
        [Required(ErrorMessage = "请输入{0}")]
        public Guid Id { get; set; }
        /// <summary>
        /// 密码（经32位MD5加密）
        /// </summary>
        [DisplayName("密码")]
        [StringLength(32, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [DisplayName("昵称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
        /// <summary>
        /// 账号状态 1.正常 2.禁用 3.锁定
        /// </summary>
        [DisplayName("用户类型")]
        [Required(ErrorMessage = "请选择{0}")]
        public int Status { get; set; }
        /// <summary>
        /// 权限id
        /// </summary>
        public List<Guid> RightsId { get; set; }
    }
    /// <summary>
    /// 个人用户修改
    /// </summary>
    public class UserInfoUpdate
    {
        /// <summary>
        /// 密码（经32位MD5加密）
        /// </summary>
        [DisplayName("密码")]
        [StringLength(32, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [DisplayName("昵称")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [DisplayName("头像地址")]
        [StringLength(300, ErrorMessage = "{0}长度超过{1}位")]
        public string Url { get; set; }
    }
}
