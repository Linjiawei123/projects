
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Models
{
    /// <summary>
    /// 用户权限
    /// </summary>
    public class UserAndPermission
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
        /// 权限ID
        /// </summary>
        [DisplayName("权限ID")]
        [Column("PermissionId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid PermissionId { get; set; }
        /// <summary>
        /// 用户
        /// </summary>
        public User User { get; set; }
    }
}
