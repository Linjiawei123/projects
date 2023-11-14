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
    public class BSAccountAdd
    {
        /// <summary>
        /// 帐户编号
        /// </summary>
        [DisplayName("帐户编号")]
        [Required(ErrorMessage = "请输入帐户编号")]
        public string AccountCode { get; set; }

        /// <summary>
        /// 帐户名称
        /// </summary>
        [DisplayName("帐户名称")]
        [Required(ErrorMessage = "请输入帐户名称")]
        public string AccountName { get; set; }

        /// <summary>
        /// 银行帐号
        /// </summary>
        [DisplayName("银行帐号")]
        public string BankAccount { get; set; }

        /// <summary>
        /// 会计科目
        /// </summary>
        [DisplayName("会计科目")]
        [Required(ErrorMessage = "请选择会计科目")]
        public int AccSubject { get; set; }

        /// <summary>
        /// 期初金额
        /// </summary>
        [DisplayName("期初金额")]
        public decimal AccMoney { get; set; } = 0;
    }
    public class BSAccountUpdate : BSAccountAdd
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DisplayName("数据id")]
        [Required]
        public int Id { get; set; }
    }
}
