using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPRPlatform.API.Models
{
    /// <summary>
    /// 角色用户
    /// </summary>
    public class RoleAndUser
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
        /// 角色ID
        /// </summary>
        [DisplayName("角色ID")]
        [Column("RoleId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid RoleId { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DisplayName("用户ID")]
        [Column("UserId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid UserId { get; set; }
    }
    /// <summary>
    /// 角色成员
    /// </summary>
    public class RoleUserInfo : RoleAndUser
    {
        /// <summary>
        /// 账号
        /// </summary>
        [DisplayName("账号")]
        public string Account { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        public string Name { get; set; }
    }
}
