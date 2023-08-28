
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace EPRPlatform.API.Models
{
    /// <summary>
    /// 上传文件
    /// </summary>
    public class UploadFile
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
        /// 文件名称
        /// </summary>
        [DisplayName("数据id")]
        [Column("FileName", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}的值超长或值不符合长度")]
        public string FileName { get; set; }
        /// <summary>
        /// 存放路径
        /// </summary>
        [DisplayName("存放路径")]
        [Column("Path", TypeName = "varchar(300)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(300, MinimumLength = 1, ErrorMessage = "{0}的值超长或值不符合长度")]
        [Newtonsoft.Json.JsonIgnore]
        public string Path { get; set; }
        /// <summary>
        /// 访问地址
        /// </summary>
        [DisplayName("访问地址")]
        [Column("Url", TypeName = "varchar(300)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(300,MinimumLength = 1, ErrorMessage = "{0}的值超长或值不符合长度")]
        public string Url { get; set; }
        /// <summary>
        /// 文件大小
        /// </summary>
        [DisplayName("文件大小")]
        [Column("FileSize", TypeName = "bigint")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public long FileSize { get; set; }
        /// <summary>
        /// 上传用户id
        /// </summary>
        [DisplayName("上传用户id")]
        [Column("UserId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 上传时间
        /// </summary>
        [DisplayName("上传时间")]
        [Column("UploadTime", TypeName = "datetime")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime UploadTime { get; set; }
    }
}
