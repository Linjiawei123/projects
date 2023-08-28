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
    /// 行业分类
    /// </summary>
    public class CUTrade
    {
        /// <summary>
        /// 行业编号
        /// </summary>
        [Key]
        [DisplayName("行业编号")]
        [Column("TradeCode", TypeName = "varchar(10)")]
        public string TradeCode { get; set; }
        /// <summary>
        /// 行业名称
        /// </summary>
        [DisplayName("行业名称")]
        [Column("TradeName", TypeName = "varchar(20)")]
        public string TradeName { get; set; }
    }
}
