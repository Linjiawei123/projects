using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Models
{
    /// <summary>
    /// 用户登录日志
    /// </summary>
    public class UserLoginLog
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        [Key]
        [DisplayName("数据ID")]
        [Column("Id", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DisplayName("用户ID")]
        [Column("UserId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        [DisplayName("登录时间")]
        [Column("LoginTime", TypeName = "datetime")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        [DisplayName("登录IP")]
        [Column("LoginIP", TypeName = "varchar(20)")]
        public string LoginIP { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
    }
    /// <summary>
    /// 登录日志输出模型
    /// </summary>
    public class UserLoginLogSimple
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        public DateTime LoginTime { get; set; }
        /// <summary>
        /// 登录IP
        /// </summary>
        public string LoginIP { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }
    }
}
