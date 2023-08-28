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
    /// 菜单
    /// </summary>
    public class Menu
    {
        /// <summary>
        /// 菜单id
        /// </summary>
        [Key]
        [DisplayName("菜单id")]
        [Column("Id", TypeName= "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
        /// <summary>
        /// 父级id
        /// </summary>
        [DisplayName("父级id")]
        [Column("ParentId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid ParentId { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Column("Name", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50,MinimumLength = 1,ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
        /// <summary>
        /// 菜单代码
        /// </summary>
        [DisplayName("菜单代码")]
        [Column("Code", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Code { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [Column("Sort", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Sort { get; set; }
        /// <summary>
        /// 跳转地址
        /// </summary>
        [DisplayName("跳转地址")]
        [Column("Url", TypeName = "varchar(50)")]
        [StringLength(50, ErrorMessage = "{0}长度超过{1}位")]
        public string Url { get; set; }
        /// <summary>
        /// 图标
        /// </summary>
        [DisplayName("图标")]
        [Column("Icon", TypeName = "varchar(20)")]
        [StringLength(20, ErrorMessage = "{0}长度超过{1}位")]
        public string Icon { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [Column("Remark", TypeName = "ncarchar(50)")]
        [StringLength(50, ErrorMessage = "{0}长度超过{1}位")]
        public string Remark { get; set; }
        /// <summary>
        /// 级别
        /// </summary>
        [DisplayName("级别")]
        [Column("Grade", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Grade { get; set; }
        /// <summary>
        /// 是否默认
        /// </summary>
        [DisplayName("是否默认")]
        [Column("IsDefault", TypeName = "bit")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool IsDefault { get; set; }
        /// <summary>
        /// 是否打开新页面
        /// </summary>
        [DisplayName("是否打开新页面")]
        [Column("IsBlank", TypeName = "bit")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool IsBlank { get; set; }
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
