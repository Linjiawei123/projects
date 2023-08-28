using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Models.Base
{
    /// <summary>
    /// 员工
    /// </summary>
    public class BSEmployee
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("数据id")]
        [Column("Id", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Id { get; set; }
        /// <summary>
        /// 员工编号
        /// </summary>
        [Key]
        [DisplayName("员工编号")]
        [Column("EmployeeCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入员工编号")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        [DisplayName("员工名称")]
        [Column("EmployeeName", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入员工名称")]
        public string EmployeeName { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [DisplayName("部门编号")]
        [Column("DepartmentCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入部门编号")]
        public string DepartmentCode { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [DisplayName("年龄")]
        [Column("Age", TypeName = "int")]
        public int? Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        [Column("Sex", TypeName = "char(1)")]
        public string Sex { get; set; }
        /// <summary>
        /// 教育水平
        /// </summary>
        [DisplayName("教育水平")]
        [Column("EduLevel", TypeName = "char(1)")]
        public string EduLevel { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        [Column("Job", TypeName = "varchar(20)")]
        public string Job { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        [DisplayName("入职日期")]
        [Column("JoinDate", TypeName = "datetime")]
        public DateTime? JoinDate { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [DisplayName("电话号码")]
        [Column("TelephoneCode", TypeName = "varchar(13)")]
        public string TelephoneCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [Column("Remark", TypeName = "text")]
        public string Remark { get; set; }
        /// <summary>
        /// 部门
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public BSDepartment BSDepartment { get; set; }
    }
}
