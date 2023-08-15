using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroService.Models
{
    /// <summary>
    /// 聊天记录
    /// </summary>
    public class ChatMessage
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
        /// 发送者id
        /// </summary>
        [DisplayName("发送者id")]
        [Column("SenderId", TypeName = "varchar(36)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string SenderId { get; set; }
        /// <summary>
        /// 接收者id
        /// </summary>
        [DisplayName("接收者id")]
        [Column("ReceiverId", TypeName = "varchar(36)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string ReceiverId { get; set; }
        /// <summary>
        /// 信息内容
        /// </summary>
        [DisplayName("信息内容")]
        [Column("Content", TypeName = "nvarchar(MAX)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Content { get; set; } = "";
        /// <summary>
        /// 信息类型
        /// </summary>
        [DisplayName("信息类型")]
        [Column("Type", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Type { get; set; } = 1;
        /// <summary>
        /// 发送时间
        /// </summary>
        [DisplayName("发送时间")]
        [Column("SendTime", TypeName = "varchar(30)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string SendTime { get; set; }
    }
}
