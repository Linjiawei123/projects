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
    /// 信用等级
    /// </summary>
    public class CUCredit
    {
        /// <summary>
        /// 信用编码
        /// </summary>
        [Key]
        [DisplayName("信用编码")]
        [Column("CreditCode", TypeName = "varchar(10)")]
        public string CreditCode { get; set; }
        /// <summary>
        /// 信用名称
        /// </summary>
        [DisplayName("信用名称")]
        [Column("CreditName", TypeName = "varchar(20)")]
        public string CreditName { get; set; }
    }
}
