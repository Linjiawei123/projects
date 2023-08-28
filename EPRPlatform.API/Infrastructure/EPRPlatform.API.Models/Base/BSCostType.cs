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
    /// 费用类型
    /// </summary>
    public class BSCostType
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
        /// 费用类型编号
        /// </summary>
        [Key]
        [DisplayName("费用类型编号")]
        [Column("CostTypeCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入费用类型编号")]
        public string CostTypeCode { get; set; }
        /// <summary>
        /// 费用类型名称
        /// </summary>
        [DisplayName("费用类型名称")]
        [Column("CostTypeName", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "请输入费用类型名称")]
        public string CostTypeName { get; set; }
        /// <summary>
        /// 费用
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public List<BSCost> BSCosts { get; set; }
    }
}
