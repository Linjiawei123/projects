using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroService.Models
{
    /// <summary>
    /// 用户
    /// </summary>
    public class User
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Key]
        [DisplayName("用户ID")]
        [Column("Id", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        [Column("Account", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Account { get; set; }
        /// <summary>
        /// 密码（经32位MD5加密）
        /// </summary>
        [DisplayName("密码")]
        [Column("PassWord", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Password { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        [DisplayName("昵称")]
        [Column("Name", TypeName = "nvarchar(10)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(10, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
        /// <summary>
        /// 头像地址
        /// </summary>
        [DisplayName("头像地址")]
        [Column("Url", TypeName = "varchar(300)")]
        [StringLength(300, ErrorMessage = "{0}长度超过{1}位")]
        public string Url { get; set; }
        /// <summary>
        /// 用户类型 1.超级管理员 2.管理员 3.普通用户
        /// </summary>
        [DisplayName("用户类型")]
        [Column("UserType", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int UserType { get; set; }
        /// <summary>
        /// 账号状态 1.正常 2.禁用 3.锁定
        /// </summary>
        [DisplayName("用户类型")]
        [Column("Status", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Status { get; set; }
        /// <summary>
        /// 账号登录错误次数，默认0
        /// </summary>
        [DisplayName("用户类型")]
        [Column("ErrTimes", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int ErrTimes { get; set; }
        /// <summary>
        /// 第一次登录异常时间
        /// </summary>
        [DisplayName("第一次登录异常时间")]
        [Column("FirstErrTime", TypeName = "datetime")]
        public DateTime? FirstErrTime { get; set; }
        /// <summary>
        /// 操作者id
        /// </summary>
        [DisplayName("操作者id")]
        [Column("OperaterId", TypeName = "uniqueidentifier")]
        public Guid OperaterId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [DisplayName("操作时间")]
        [Column("OperateTime", TypeName = "datetime")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 权限
        /// </summary>
        public List<UserAndPermission> Rights { get; set; }
        /// <summary>
        /// 登录日志
        /// </summary>
        public List<UserLoginLog> LoginLog { get; set; }
        /// <summary>
        /// 操作日志
        /// </summary>
        public List<OperateLog> OperateLog { get; set; }
    }
}
