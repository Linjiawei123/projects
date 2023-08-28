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
    /// 
    /// </summary>
    public class CUChance
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        [DisplayName("")]
        [Column("ChanceCode", TypeName = "varchar(10)")]
        public string ChanceCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [DisplayName("")]
        [Column("ChanceName", TypeName = "varchar(20)")]
        public string ChanceName { get; set; }
    }
}
