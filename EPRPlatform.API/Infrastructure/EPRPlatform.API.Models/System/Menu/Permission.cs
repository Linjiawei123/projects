using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Models
{
    /// <summary>
    /// 权限
    /// </summary>
    public class Permission
    {
        /// <summary>
        /// 权限ID
        /// </summary>
        [Key]
        [DisplayName("权限ID")]
        [Column("Id", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
        /// <summary>
        /// 菜单ID
        /// </summary>
        [DisplayName("菜单ID")]
        [Column("MenuId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid MenuId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Column("Name", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        /// <summary>
        /// 权限代码
        /// </summary>
        [DisplayName("权限代码")]
        [Column("Code", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1)]
        public string Code { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [Column("Remark", TypeName = "nvarchar(100)")]
        [StringLength(100)]
        public string Remark { get; set; }
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
    }
}
