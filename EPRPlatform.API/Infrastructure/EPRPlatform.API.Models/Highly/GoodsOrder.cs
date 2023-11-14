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
    /// 商品订单
    /// </summary>
    public class GoodsOrder
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
        /// 商品id
        /// </summary>
        [DisplayName("商品id")]
        [Column("GoodsId", TypeName = "uniqueidentifier")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public Guid GoodsId { get; set; }
        /// <summary>
        /// 编号
        /// </summary>
        [DisplayName("编号")]
        [Column("Code", TypeName = "nvarchar(50)")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public string Code { get; set; }
        /// <summary>
        /// 数量
        /// </summary>
        [DisplayName("数量")]
        [Column("Number", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Number { get; set; }
        /// <summary>
        /// 下单时间
        /// </summary>
        [DisplayName("下单时间")]
        [Column("Time", TypeName = "datetime")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public DateTime Time { get; set; }
    }
}
