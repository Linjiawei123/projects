
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;


namespace EPRPlatform.API.Dto.OAModels
{
    /// <summary>
    /// 新增
    /// </summary>
    public class EmployeeContractAdd
    {
        /// <summary>
        /// 职工id
        /// </summary>
        [DisplayName("职工")]
        [Required(ErrorMessage = "请选择{0}")]
        public Guid EmployeeId { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        [DisplayName("合同编号")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, ErrorMessage = "{0}输入长度有误")]
        public string Code { get; set; }
        /// <summary>
        /// 合同开始时间
        /// </summary>
        [DisplayName("合同开始时间")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime StartDate { get; set; }
        /// <summary>
        /// 合同结束时间
        /// </summary>
        [DisplayName("合同结束时间")]
        public DateTime? EndDate { get; set; }
        /// <summary>
        /// 工资
        /// </summary>
        [DisplayName("工资")]
        [Required(ErrorMessage = "请输入{0}")]
        public decimal Salary { get; set; }
        /// <summary>
        /// 双方约定
        /// </summary>
        [DisplayName("双方约定")]
        [StringLength(500, ErrorMessage = "{0}输入长度有误")]
        public string Agree { get; set; }
        /// <summary>
        /// 合同签订时间
        /// </summary>
        [DisplayName("合同签订时间")]
        [Required(ErrorMessage = "请输入{0}")]
        public DateTime SigningDate { get; set; }
        /// <summary>
        /// 合同文件路径
        /// </summary>
        [DisplayName("合同文件路径")]
        [StringLength(300, ErrorMessage = "{0}长度有误")]
        public string FilePath { get; set; }
    }
    /// <summary>
    /// 修改
    /// </summary>
    public class EmployeeContractUpdate : EmployeeContractAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
    }
}
