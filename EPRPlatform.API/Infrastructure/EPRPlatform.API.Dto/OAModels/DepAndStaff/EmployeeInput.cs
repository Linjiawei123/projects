using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace EPRPlatform.API.Dto.OAModels
{
    /// <summary>
    /// 新增
    /// </summary>
    public class EmployeeAdd
    {
        /// <summary>
        /// 姓名
        /// </summary>
        [DisplayName("姓名")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0}输入长度有误")]
        public string Name { get; set; }
        /// <summary>
        /// 职工编号
        /// </summary>
        [DisplayName("职工编号")]
        [Required(ErrorMessage = "请输入{0}")]
        [StringLength(20, MinimumLength = 1, ErrorMessage = "{0}输入长度有误")]
        public string Code { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        [DisplayName("身份证")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}输入长度有误")]
        public string IdentityCard { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        [DisplayName("籍贯")]
        [Required(ErrorMessage = "请输入{0}的值")]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "{0}输入长度有误")]
        public string NativePlace { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [DisplayName("详细地址")]
        [StringLength(200, ErrorMessage = "{0}输入长度有误")]
        public string Address { get; set; }
        /// <summary>
        /// 证件照
        /// </summary>
        [DisplayName("证件照")]
        [StringLength(300, ErrorMessage = "{0}输入长度有误")]
        public string Photo { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DisplayName("电子邮箱")]
        [StringLength(100, ErrorMessage = "{0}输入长度有误")]
        public string Email { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        [StringLength(20, ErrorMessage = "{0}输入长度有误")]
        public string Phone { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        [DisplayName("入职日期")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime HireDate { get; set; }
        /// <summary>
        /// 职位、岗位
        /// </summary>
        [DisplayName("职位、岗位")]
        [StringLength(50, ErrorMessage = "{0}输入长度有误")]
        public string Job { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [StringLength(150, ErrorMessage = "{0}输入长度有误")]
        public string Remark { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        [DisplayName("部门id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [DisplayName("用户id")]
        public Guid? UserId { get; set; }
    }
    /// <summary>
    /// 修改
    /// </summary>
    public class EmployeeUpdate : EmployeeAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
    }
}
