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
    /// 部门
    /// </summary>
    public class BSDepartment
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
        /// 部门编号
        /// </summary>
        [Key]
        [DisplayName("部门编号")]
        [Column("DepartmentCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入{0}")]
        public string DepartmentCode { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        [DisplayName("部门名称")]
        [Column("DepartmentName", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "请输入{0}")]
        public string DepartmentName { get; set; }
        /// <summary>
        /// 员工
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<BSEmployee> BSEmployees { get; set; }
    }
}
