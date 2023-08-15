using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace MicroService.Models
{
    /// <summary>
    /// 群聊组
    /// </summary>
    public class ChatGroup
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
        /// 群名
        /// </summary>
        [DisplayName("群名")]
        [Column("Name", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}长度超过{1}位或输入了空字符串")]
        public string Name { get; set; }
        /// <summary>
        /// 群头像
        /// </summary>
        [DisplayName("群头像")]
        [Column("Url", TypeName = "nvarchar(300)")]
        [StringLength(300, ErrorMessage = "{0}长度超过{1}位")]
        public string Url { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [Column("Remark", TypeName = "nvarchar(200)")]
        [StringLength(200, ErrorMessage = "{0}长度超过{1}位")]
        public string Remark { get; set; }
        /// <summary>
        /// 群成员
        /// </summary>
        public List<ChatGroupUser> User { get; set; }
    }
}
