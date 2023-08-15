using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MicroService.Models
{
    /// <summary>
    /// 角色
    /// </summary>
    public class Role
    {
        /// <summary>
        /// 角色ID
        /// </summary>
        [Key]
        [DisplayName("角色ID")]
        [Column("Id", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Column("Name", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1)]
        public string Name { get; set; }
        /// <summary>
        /// 角色编号
        /// </summary>
        [DisplayName("角色编号")]
        [Column("Code", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1)]
        public string Code { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        [Column("Status", TypeName = "bit")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool Status { get; set; }
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
        /// <summary>
        /// 角色权限
        /// </summary>
        public List<RoleAndPermission> Rights { get; set; }
    }
}
