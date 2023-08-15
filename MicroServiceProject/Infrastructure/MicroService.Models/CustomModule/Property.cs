using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Models
{
    /// <summary>
    /// 模块属性
    /// </summary>
    public class Property : IDeletable
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
        /// 模块id
        /// </summary>
        [DisplayName("模块id")]
        [Column("ModuleId", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int ModuleId { get; set; }
        /// <summary>
        /// 属性标题
        /// </summary>
        [DisplayName("属性标题")]
        [Column("Title", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Title { get; set; }
        /// <summary>
        /// 属性值类型
        /// </summary>
        [DisplayName("属性值类型")]
        [Column("Type", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Type { get; set; }
        /// <summary>
        /// 排序
        /// </summary>
        [DisplayName("排序")]
        [Column("Sort", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Sort { get; set; }
        /// <summary>
        /// 模块
        /// </summary>
        public Module CustomModule { get; set; }
        /// <summary>
        /// 是否删除
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
