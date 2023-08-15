using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MicroService.Models
{
    /// <summary>
    /// 角色权限
    /// </summary>
    public class RoleAndPermission
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
        /// 权限ID
        /// </summary>
        [DisplayName("权限ID")]
        [Column("PermissionId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid PermissionId { get; set; }
        /// <summary>
        /// 角色
        /// </summary>
        public Role Role { get; set; }
    }
}
