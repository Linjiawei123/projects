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
    /// 供应商
    /// </summary>
    public class BSSupplier
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
        /// 供应商代码
        /// </summary>
        [Key]
        [DisplayName("供应商代码")]
        [Column("SupplierCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入供应商代码")]
        public string SupplierCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [DisplayName("供应商名称")]
        [Column("SupplierName", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "请输入供应商名称")]
        public string SupplierName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        [Column("TelephoneCode", TypeName = "varchar(13)")]
        public string TelephoneCode { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        [Column("Email", TypeName = "varchar(20)")]
        public string Email { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        [Column("PostCode", TypeName = "varchar(6)")]
        public string PostCode { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        [Column("Linkman", TypeName = "varchar(10)")]
        public string Linkman { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        [DisplayName("网址")]
        [Column("Url", TypeName = "varchar(50)")]
        public string Url { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        [Column("Address", TypeName = "varchar(100)")]
        public string Address { get; set; }
    }
}
