using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Models
{
    /// <summary>
    /// 自定义模块
    /// </summary>
    public class Module
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [Key]
        [DisplayName("数据id")]
        [Column("Id", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Id { get; set; }
        /// <summary>
        /// 菜单id
        /// </summary>
        [DisplayName("菜单ID")]
        [Column("MenuId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid MenuId { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [DisplayName("模块名称")]
        [Column("Name", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
        /// <summary>
        /// 新增用户id
        /// </summary>
        [DisplayName("新增用户id")]
        [Column("AddUserId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid AddUserId { get; set; }
        /// <summary>
        /// 新增时间
        /// </summary>
        [DisplayName("新增时间")]
        [Column("AddTime", TypeName = "datetime")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime AddTime { get; set; }
        /// <summary>
        /// 最后一次修改用户id
        /// </summary>
        [DisplayName("最后一次修改用户id")]
        [Column("LastOperaterId", TypeName = "uniqueidentifier")]
        [Newtonsoft.Json.JsonIgnore]
        public Guid? LastOperaterId { get; set; }
        /// <summary>
        /// 最后一次修改时间
        /// </summary>
        [DisplayName("最后一次修改时间")]
        [Column("LastOperateTime", TypeName = "datetime")]
        [Newtonsoft.Json.JsonIgnore]
        public DateTime? LastOperateTime { get; set; }
        /// <summary>
        /// 模型属性
        /// </summary>
        public List<Property> CustomPropertys { get; set; }
    }
}
