using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Models.CustomModule
{
    /// <summary>
    /// 自定义模块值
    /// </summary>
    public class PropertyValue
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
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Column("DataId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid DataId { get; set; }
        /// <summary>
        /// 属性id
        /// </summary>
        [DisplayName("属性id")]
        [Column("PropertyId", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int PropertyId { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        [DisplayName("属性值")]
        [Column("Value", TypeName = "nvarchar(MAX)")]
        public string Value { get; set; }
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
        [Required(ErrorMessage = "请输入{0}的值")]
        [Newtonsoft.Json.JsonIgnore]
        public DateTime? LastOperateTime { get; set; }
    }
}
