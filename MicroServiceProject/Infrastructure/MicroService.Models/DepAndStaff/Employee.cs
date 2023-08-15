
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace MicroService.Models
{
    /// <summary>
    /// 职工
    /// </summary>
    public class Employee : IDeletable
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
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Column("Name", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Name { get; set; }
        /// <summary>
        /// 职工编号
        /// </summary>
        [DisplayName("职工编号")]
        [Column("Code", TypeName = "nvarchar(20)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Code { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        [Column("Sex", TypeName = "bit")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public bool Sex { get; set; }
        /// <summary>
        /// 生日
        /// </summary>
        [DisplayName("生日")]
        [Column("Birthday", TypeName = "date")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime Birthday { get; set; }
        /// <summary>
        /// 身份证
        /// </summary>
        [DisplayName("身份证")]
        [Column("IdentityCard", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string IdentityCard { get; set; }
        /// <summary>
        /// 籍贯
        /// </summary>
        [DisplayName("籍贯")]
        [Column("NativePlace", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string NativePlace { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        [DisplayName("详细地址")]
        [Column("Address", TypeName = "nvarchar(200)")]
        public string Address { get; set; }
        /// <summary>
        /// 证件照
        /// </summary>
        [DisplayName("证件照")]
        [Column("Photo", TypeName = "varchar(300)")]
        public string Photo { get; set; }
        /// <summary>
        /// 电子邮箱
        /// </summary>
        [DisplayName("电子邮箱")]
        [Column("Email", TypeName = "nvarchar(100)")]
        public string Email { get; set; }
        /// <summary>
        /// 手机号码
        /// </summary>
        [DisplayName("手机号码")]
        [Column("Phone", TypeName = "varchar(20)")]
        public string Phone { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        [DisplayName("入职日期")]
        [Column("HireDate", TypeName = "date")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime HireDate { get; set; }
        /// <summary>
        /// 职位、岗位
        /// </summary>
        [DisplayName("职位、岗位")]
        [Column("Job", TypeName = "nvarchar(50)")]
        public string Job { get;set; }
        /// <summary>
        /// 简介
        /// </summary>
        [DisplayName("简介")]
        [Column("Remark", TypeName = "nvarchar(150)")]
        public string Remark { get; set; }
        /// <summary>
        /// 部门id
        /// </summary>
        [DisplayName("部门id")]
        [Column("DepartmentId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid DepartmentId { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        [DisplayName("用户id")]
        [Column("UserId", TypeName = "uniqueidentifier")]
        public Guid? UserId { get; set; }
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
    /// 职工详情
    /// </summary>
    public class EmployeeInfo : Employee
    {
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
