using IdentityServer4.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPRPlatform.API.Dto.BaseModels
{
    public class BSCustomerAdd
    {
        /// <summary>
        /// 客户代码
        /// </summary>
        [DisplayName("客户代码")]
        [Required(ErrorMessage = "请输入客户代码")]
        public string CustomerCode { get; set; }
        /// <summary>
        /// 客户名称
        /// </summary>
        [DisplayName("客户名称")]
        [Required(ErrorMessage = "请输入客户名称")]
        public string CustomerName { get; set; }
        /// <summary>
        /// 业务员
        /// </summary>
        [DisplayName("业务员")]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// 法人
        /// </summary>
        [DisplayName("法人")]
        public string AtrMan { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        public string TelephoneCode { get; set; }
        /// <summary>
        /// 传真号码
        /// </summary>
        [DisplayName("传真号码")]
        public string FaxCode { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        public string PostCode { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 联系人
        /// </summary>
        [DisplayName("联系人")]
        public string Linkman { get; set; }
        /// <summary>
        /// 网址
        /// </summary>
        [DisplayName("网址")]
        public string Url { get; set; }
        /// <summary>
        /// 地址
        /// </summary>
        [DisplayName("地址")]
        public string Address { get; set; }
        /// <summary>
        /// 客户等级
        /// </summary>
        [DisplayName("客户等级")]
        public string GradeCode { get; set; }
        /// <summary>
        /// 客户状态
        /// </summary>
        [DisplayName("客户状态")]
        public string StateCode { get; set; }
        /// <summary>
        /// 信用等级
        /// </summary>
        [DisplayName("信用等级")]
        public string CreditCode { get; set; }
        /// <summary>
        /// 行业
        /// </summary>
        [DisplayName("行业")]
        public string TradeCode { get; set; }
    }
    public class BSCustomerUpdate : BSCustomerAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required]
        public int Id { get; set; }
    }
}
