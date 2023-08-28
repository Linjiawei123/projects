using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EPRPlatform.API.Dto.OAModels
{
    /// <summary>
    /// 职工简化模型
    /// </summary>
    public class EmployeeSimple
    {
        /// <summary>
        /// 数据id
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 职工编号
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        public string Birthday { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        public string IdentityCard { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        public string HireDate { get; set; }
        /// <summary>
        /// 职位、岗位
        /// </summary>
        public string Job { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        public string Department { get; set; }
        /// <summary>
        /// 用户账号
        /// </summary>
        public string Account { get; set; }
    }
}
