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
    /// 客户状态
    /// </summary>
    public class CUState
    {
        /// <summary>
        /// 状态编码
        /// </summary>
        [Key]
        [DisplayName("状态编码")]
        [Column("StateCode", TypeName = "varchar(10)")]
        public string StateCode { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        [DisplayName("状态")]
        [Column("StateName", TypeName = "varchar(20)")]
        public string StateName { get; set; }
    }
}
