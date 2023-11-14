using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Models.Highly
{
    /// <summary>
    /// 商品
    /// </summary>
    public class Goods
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [Key]
        [DisplayName("数据id")]
        [Column("Id", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid Id { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        [Column("Code", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Code { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        [DisplayName("名称")]
        [Column("Name", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Name { get; set; }
        /// <summary>
        /// 库存
        /// </summary>
        [DisplayName("库存")]
        [Column("Inventory", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Inventory { get; set; }
    }
}
