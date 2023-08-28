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
    /// 费用
    /// </summary>
    public class BSCost
    {
        /// <summary>
        /// 费用编号
        /// </summary>
        [Key]
        [DisplayName("费用编号")]
        [Column("CostCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入费用类型编号")]
        public string CostCode { get; set; }
        /// <summary>
        /// 费用名称
        /// </summary>
        [DisplayName("费用名称")]
        [Column("CostName", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "请输入费用类型编号")]
        public string CostName { get; set; }
        /// <summary>
        /// 费用类型编号
        /// </summary>
        [DisplayName("费用类型编号")]
        [Column("CostTypeCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入费用类型编号")]
        public string CostTypeCode { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [Column("Remark", TypeName = "varchar(10)")]
        public string Remark { get; set; }
        /// <summary>
        /// 类型
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public BSCostType BSCostType { get; set; }
    }
}
