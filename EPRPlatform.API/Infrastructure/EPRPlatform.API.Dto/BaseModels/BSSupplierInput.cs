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
    public class BSSupplierAdd
    {
        /// <summary>
        /// 供应商代码
        /// </summary>
        [DisplayName("供应商代码")]
        [Required(ErrorMessage = "请输入供应商代码")]
        public string SupplierCode { get; set; }
        /// <summary>
        /// 供应商名称
        /// </summary>
        [DisplayName("供应商名称")]
        [Required(ErrorMessage = "请输入供应商名称")]
        public string SupplierName { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        [DisplayName("联系电话")]
        public string TelephoneCode { get; set; }
        /// <summary>
        /// 邮箱
        /// </summary>
        [DisplayName("邮箱")]
        public string Email { get; set; }
        /// <summary>
        /// 邮政编码
        /// </summary>
        [DisplayName("邮政编码")]
        public string PostCode { get; set; }
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
    }
    public class BSSupplierUpdate : BSSupplierAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required]
        public int Id { get; set; }
    }
}
