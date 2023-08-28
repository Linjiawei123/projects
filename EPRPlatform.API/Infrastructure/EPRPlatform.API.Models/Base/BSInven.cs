using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Models.Base
{
    /// <summary>
    /// 货存档案
    /// </summary>
    public class BSInven
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("数据id")]
        [Column("Id", TypeName = "bigint")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public long Id { get; set; }
        /// <summary>
        /// 存货编码
        /// </summary>
        [Key]
        [DisplayName("数据id")]
        [Column("InvenCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string InvenCode { get; set; }
        /// <summary>
        /// 存货名称
        /// </summary>
        [DisplayName("存货名称")]
        [Column("InvenName", TypeName = "varchar(40)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string InvenName { get; set; }
        /// <summary>
        /// 存货类别
        /// </summary>
        [DisplayName("存货类别")]
        [Column("InvenTypeCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string InvenTypeCode { get; set; }
        /// <summary>
        /// 规格型号
        /// </summary>
        [DisplayName("规格型号")]
        [Column("SpecsModel", TypeName = "varchar(30)")]
        public string SpecsModel { get; set; }
        /// <summary>
        /// 计量单位
        /// </summary>
        [DisplayName("计量单位")]
        [Column("MeaUnit", TypeName = "varchar(10)")]
        public string MeaUnit { get; set; }
        /// <summary>
        /// 参考售价
        /// </summary>
        [DisplayName("参考售价")]
        [Column("SelPrice", TypeName = "decimal(12,2)")]
        public decimal? SelPrice { get; set; }
        /// <summary>
        /// 参考进价
        /// </summary>
        [DisplayName("参考进价")]
        [Column("PurPrice", TypeName = "decimal(12,2)")]
        public decimal? PurPrice { get; set; }
        /// <summary>
        /// 最低库存
        /// </summary>
        [DisplayName("最低库存")]
        [Column("SmallStockNum", TypeName = "int")]
        public int? SmallStockNum { get; set; }
        /// <summary>
        /// 最高库存
        /// </summary>
        [DisplayName("最高库存")]
        [Column("BigStockNum", TypeName = "int")]
        public int? BigStockNum { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        [Newtonsoft.Json.JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public BSInvenType BSInvenType { get; set; }
    }
}
