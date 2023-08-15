using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Models
{
    /// <summary>
    /// 错误日志
    /// </summary>
    public class ErrorLog
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("数据id")]
        [Column("Id", TypeName = "bigint")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public long Id { get; set; }
        /// <summary>
        /// 错误信息
        /// </summary>
        [DisplayName("错误信息")]
        [Column("Message", TypeName = "nvarchar(500)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(500, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Message { get; set; }
        /// <summary>
        /// 错误源
        /// </summary>
        [DisplayName("错误源")]
        [Column("Source", TypeName = "nvarchar(max)")]
        public string Source { get; set; }
        /// <summary>
        /// 错误堆栈
        /// </summary>
        [DisplayName("错误堆栈")]
        [Column("StackTrace", TypeName = "nvarchar(max)")]
        public string StackTrace { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [DisplayName("添加时间")]
        [Column("AddTime", TypeName = "datetime")]
        public DateTime AddTime { get; set; }
    }
}
