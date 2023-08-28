using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EPRPlatform.API.Models
{
    /// <summary>
    /// 操作日志
    /// </summary>
    public class OperateLog
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        [Key]
        [DisplayName("数据ID")]
        [Column("Id", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        [DisplayName("用户ID")]
        [Column("UserId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid UserId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [DisplayName("操作时间")]
        [Column("OperateTime", TypeName = "datetime")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        [DisplayName("操作")]
        [Column("Operate", TypeName = "int")]
        public int Operate { get; set; }
        /// <summary>
        /// 操作模块
        /// </summary>
        [DisplayName("操作模块")]
        [Column("Module", TypeName = "int")]
        public int Module { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        [DisplayName("请求方法")]
        [Column("Method", TypeName = "varchar(10)")]
        public string Method { get; set; }
        /// <summary>
        /// 请求路径
        /// </summary>
        [DisplayName("请求路径")]
        [Column("Path", TypeName = "varchar(200)")]
        public string Path { get; set; }
        /// <summary>
        /// 请求参数
        /// </summary>
        [DisplayName("请求参数")]
        [Column("QueryString", TypeName = "nvarchar(200)")]
        public string QueryString { get; set; }
        /// <summary>
        /// 请求体
        /// </summary>
        [DisplayName("请求体")]
        [Column("RequestBody", TypeName = "nvarchar(Max)")]
        public string RequestBody { get; set; }
        /// <summary>
        /// 状态码
        /// </summary>
        [DisplayName("状态码")]
        [Column("StatusCode", TypeName = "int")]
        public int StatusCode { get; set; }
        /// <summary>
        /// 响应体
        /// </summary>
        [DisplayName("响应体")]
        [Column("ResponseBody", TypeName = "nvarchar(Max)")]
        public string ResponseBody { get; set; }
        /// <summary>
        /// 响应时长
        /// </summary>
        [DisplayName("响应时长")]
        [Column("DurationMs", TypeName = "bigint")]
        public long DurationMs { get; set; }
    }

    /// <summary>
    /// 操作日志输出模型
    /// </summary>
    public class OperateLogSimple
    {
        /// <summary>
        /// 数据ID
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 用户ID
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public int Operate { get; set; }
        /// <summary>
        /// 操作
        /// </summary>
        public string OperateName { get; set; }
        /// <summary>
        /// 操作模块
        /// </summary>
        public int Module { get; set; }
        /// <summary>
        /// 操作模块
        /// </summary>
        public string ModuleName { get; set; }
        /// <summary>
        /// 请求方法
        /// </summary>
        public string Method { get; set; }
        /// <summary>
        /// 请求路径
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 昵称
        /// </summary>
        public string Name { get; set; }
    }
}
