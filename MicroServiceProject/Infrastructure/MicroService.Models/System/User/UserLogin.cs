
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Models
{
    /// <summary>
    /// 用户登录信息
    /// </summary>
    public class UserLogin
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
        /// 登录token
        /// </summary>
        [DisplayName("登录token")]
        [Column("Access_token", TypeName = "varchar(Max)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Access_token { get; set; }
        /// <summary>
        /// token有效时长（秒）
        /// </summary>
        [DisplayName("token有效时长")]
        [Column("Expires_in", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Expires_in { get; set; }
        /// <summary>
        /// 刷新token
        /// </summary>
        [DisplayName("刷新token")]
        [Column("Refresh_token", TypeName = "varchar(1000)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Refresh_token { get; set; }
        /// <summary>
        /// 登录时间
        /// </summary>
        [DisplayName("登录时间")]
        [Column("LoginTime", TypeName = "datetime")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime LoginTime { get; set; }
    }
}
