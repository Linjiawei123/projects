
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EPRPlatform.API.Models.Base
{
    /// <summary>
    /// 货存分类
    /// </summary>
    public class BSInvenType
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("数据id")]
        [Column("Id", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Id { get; set; }
        /// <summary>
        /// 类别代码
        /// </summary>
        [Key]
        [DisplayName("类别代码")]
        [Column("InvenTypeCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入{0}")]
        public string InvenTypeCode { get; set; }
        /// <summary>
        /// 类别名称
        /// </summary>
        [DisplayName("类别名称")]
        [Column("InvenTypeName", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "请输入{0}")]
        public string InvenTypeName { get; set; }
        /// <summary>
        /// 货存档案
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<BSInven> BSInvens { get; set; }
    }
}
