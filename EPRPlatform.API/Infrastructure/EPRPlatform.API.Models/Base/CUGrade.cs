using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace EPRPlatform.API.Models.Base
{
    /// <summary>
    /// 客户等级
    /// </summary>
    public class CUGrade
    {
        /// <summary>
        /// 客户等级编码
        /// </summary>
        [Key]
        [DisplayName("客户等级编码")]
        [Column("GradeCode", TypeName = "varchar(10)")]
        public string GradeCode { get; set; }
        /// <summary>
        /// 客户等级名称
        /// </summary>
        [DisplayName("客户等级名称")]
        [Column("GradeName", TypeName = "varchar(20)")]
        public string GradeName { get; set; }
    }
}
