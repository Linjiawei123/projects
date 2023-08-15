
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace MicroService.Models
{
    /// <summary>
    /// 群组成员
    /// </summary>
    public class ChatGroupUser
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
        /// 群组id
        /// </summary>
        [DisplayName("群组id")]
        [Column("GroupId", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int GroupId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [DisplayName("用户id")]
        [Column("UserId", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int UserId { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        [DisplayName("添加时间")]
        [Column("AddTime", TypeName = "datetime")]
        public DateTime AddTime { get; set; }
    }
}
