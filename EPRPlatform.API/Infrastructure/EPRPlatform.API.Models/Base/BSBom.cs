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
    /// 物料
    /// </summary>
    public class BSBom
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Column("Id", TypeName = "int")]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        /// <summary>
        /// 母件代码
        /// </summary>
        [DisplayName("母件代码")]
        [Column("ProInvenCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入母件代码")]
        public string ProInvenCode { get; set; }
        /// <summary>
        /// 子件代码
        /// </summary>
        [DisplayName("子件代码")]
        [Column("MatInvenCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入子件代码")]
        public string MatInvenCode { get; set; }
        /// <summary>
        /// 子件数量
        /// </summary>
        [DisplayName("子件数量")]
        [Column("Quantity", TypeName = "int")]
        public int Quantity { get; set; }
    }
}
