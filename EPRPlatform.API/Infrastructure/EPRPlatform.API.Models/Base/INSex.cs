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
    /// 性别
    /// </summary>
    public class INSex
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        [DisplayName("编号")]
        [Column("Code", TypeName = "char(1)")]
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Column("Name", TypeName = "char(2)")]
        public string Name { get; set; }
    }
}
