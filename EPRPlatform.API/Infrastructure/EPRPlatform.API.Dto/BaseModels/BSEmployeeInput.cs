using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPRPlatform.API.Models.Base;

namespace EPRPlatform.API.Dto.BaseModels
{
    public class BSEmployeeAdd
    {
        /// <summary>
        /// 员工编号
        /// </summary>
        [DisplayName("员工编号")]
        [Required(ErrorMessage = "请输入员工编号")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// 员工名称
        /// </summary>
        [DisplayName("员工名称")]
        [Required(ErrorMessage = "请输入员工名称")]
        public string EmployeeName { get; set; }
        /// <summary>
        /// 部门编号
        /// </summary>
        [DisplayName("部门编号")]
        [Required(ErrorMessage = "请输入部门编号")]
        public string DepartmentCode { get; set; }
        /// <summary>
        /// 年龄
        /// </summary>
        [DisplayName("年龄")]
        public int? Age { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        [DisplayName("性别")]
        public string Sex { get; set; }
        /// <summary>
        /// 教育水平
        /// </summary>
        [DisplayName("教育水平")]
        public string EduLevel { get; set; }
        /// <summary>
        /// 职位
        /// </summary>
        [DisplayName("职位")]
        public string Job { get; set; }
        /// <summary>
        /// 入职日期
        /// </summary>
        [DisplayName("入职日期")]
        public DateTime? JoinDate { get; set; }
        /// <summary>
        /// 电话号码
        /// </summary>
        [DisplayName("电话号码")]
        public string TelephoneCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        public string Remark { get; set; }
    }
    public class BSEmployeeUpdate : BSEmployeeAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required]
        public int Id { get; set; }
    }
}
