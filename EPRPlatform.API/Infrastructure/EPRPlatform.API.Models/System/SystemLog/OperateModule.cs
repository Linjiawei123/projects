using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EPRPlatform.API.Models
{
    /// <summary>
    /// 操作模块
    /// </summary>
    public class OperateModule
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("数据id")]
        [Column("Id", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Id { get; set; }
        /// <summary>
        /// 模块名称
        /// </summary>
        [DisplayName("模块名称")]
        [Column("Module", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Module { get; set; }
        /// <summary>
        /// 模块代码
        /// </summary>
        [DisplayName("模块代码")]
        [Column("Code", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Code { get; set; }
    }
}
