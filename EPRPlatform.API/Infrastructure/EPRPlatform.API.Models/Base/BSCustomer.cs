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
    /// 客户
    /// </summary>
    public class BSCustomer
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
        /// 客户代码
        /// </summary>
        [Key]
        [DisplayName("客户代码")]
        [Column("CustomerCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入客户代码")]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        [Column("CustomerName", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "请输入客户名称")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        [DisplayName("业务员")]
        [Column("EmployeeCode", TypeName = "varchar(10)")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        [DisplayName("法人")]
        [Column("AtrMan", TypeName = "varchar(10)")]
        public string AtrMan { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        [Column("TelephoneCode", TypeName = "varchar(13)")]
        public string TelephoneCode { get; set; }
        /// <summary>
        /// 传真号码
        /// </summary>
        [DisplayName("传真号码")]
        [Column("FaxCode", TypeName = "varchar(13)")]
        public string FaxCode { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        [Column("PostCode", TypeName = "varchar(6)")]
        public string PostCode { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        [Column("Email", TypeName = "varchar(20)")]
        public string Email { get; set; }
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
        /// <summary>
        /// 客户等级
        /// </summary>
        [DisplayName("客户等级")]
        [Column("GradeCode", TypeName = "varchar(10)")]
        public string GradeCode { get; set; }
        /// <summary>
        /// 客户状态
        /// </summary>
        [DisplayName("客户状态")]
        [Column("StateCode", TypeName = "varchar(10)")]
        public string StateCode { get; set; }
        /// <summary>
        /// 信用等级
        /// </summary>
        [DisplayName("信用等级")]
        [Column("CreditCode", TypeName = "varchar(10)")]
        public string CreditCode { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        [DisplayName("行业")]
        [Column("TradeCode", TypeName = "varchar(10)")]
        public string TradeCode { get; set; }
    }
}
