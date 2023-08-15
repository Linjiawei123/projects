using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroService.Models
{
    /// <summary>
    /// 部门
    /// </summary>
    public class Department : IDeletable
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [Key]
        [DisplayName("数据id")]
        [Column("Id", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
        /// <summary>
        /// 上级部门id
        /// </summary>
        [DisplayName("上级部门id")]
        [Column("ParentId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid ParentId { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        [Column("Name", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Name { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [DisplayName("部门编号")]
        [Column("Code", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Code { get; set; }
        /// <summary>
        /// 部门主管
        /// </summary>
        [DisplayName("部门主管")]
        [Column("ManagerId", TypeName = "uniqueidentifier")]
        public Guid ManagerId { get; set; }
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
        /// 是否已删除
        /// </summary>
        [DisplayName("是否已删除")]
        [Column("IsDeleted", TypeName = "bit")]
        public bool IsDeleted { get; set; }
    }
}
