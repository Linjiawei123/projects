namespace MicroService.Dto.OAModels
{
    /// <summary>
    /// 输出模型
    /// </summary>
    public class EmployeeContractOutData
    {
        /// <summary>
        /// 数据id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 合同编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 合同开始时间
        /// </summary>
        public string StartDate { get; set; }
        /// <summary>
        /// 合同结束时间
        /// </summary>
        public string EndDate { get; set; }
        /// <summary>
        /// 工资
        /// </summary>
        public decimal Salary { get; set; }
        /// <summary>
        /// 双方约定
        /// </summary>
        public string Agree { get; set; }
        /// <summary>
        /// 合同签订时间
        /// </summary>
        public string SigningDate { get; set; }
        /// <summary>
        /// 合同文件路径
        /// </summary>
        public string FilePath { get; set; }
        /// <summary>
        /// 职工名称
        /// </summary>
        public string Name { get; set; }
    }
}
