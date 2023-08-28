
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EPRPlatform.API.Models
{
    /// <summary>
    /// 职工劳动合同
    /// </summary>
    public class EmployeeContract : IDeletable
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
        /// 职工id
        /// </summary>
        [DisplayName("职工id")]
        [Column("EmployeeId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [Column("Code", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Code { get; set; }
        /// <summary>
        /// 合同开始时间
        /// </summary>
        [DisplayName("合同开始时间")]
        [Column("StartDate", TypeName = "date")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 合同结束时间
        /// </summary>
        [DisplayName("合同结束时间")]
        [Column("EndDate", TypeName = "date")]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 工资
        /// </summary>
        [DisplayName("工资")]
        [Column("Salary", TypeName = "decimal(10, 2)")]
        public decimal Salary { get;set; }
        /// <summary>
        /// 双方约定
        /// </summary>
        [DisplayName("双方约定")]
        [Column("Agree", TypeName = "nvarchar(500)")]
        public string Agree { get; set; }
        /// <summary>
        /// 合同签订时间
        /// </summary>
        [DisplayName("合同签订时间")]
        [Column("SigningDate", TypeName = "date")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime SigningDate { get; set; }
        /// <summary>
        /// 合同文件路径
        /// </summary>
        [DisplayName("合同文件路径")]
        [Column("FilePath", TypeName = "varchar(300)")]
        public string FilePath { get; set; }
        /// <summary>
        /// 操作者id
        /// </summary>
        [DisplayName("操作者id")]
        [Column("OperaterId", TypeName = "uniqueidentifier")]
        public Guid OperaterId { get; set; }
        /// <summary>
        /// 操作时间
        /// </summary>
        [DisplayName("操作时间")]
        [Column("OperateTime", TypeName = "datetime")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime OperateTime { get; set; }
        /// <summary>
        /// 是否已删除
        /// </summary>
        [DisplayName("是否已删除")]
        [Column("IsDeleted", TypeName = "bit")]
        public bool IsDeleted { get; set; }
    }
    /// <summary>
    /// 输出模型
    /// </summary>
    public class EmployeeContractSimple : EmployeeContract
    {
        /// <summary>
        /// 职工名称
        /// </summary>
        public string Name { get; set; }
    }
}
