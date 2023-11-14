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
    /// 帐户
    /// </summary>
    public class BSAccount
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
        /// 帐户编号
        /// </summary>
        [DisplayName("帐户编号")]
        [Column("AccountCode", TypeName = "varchar(19)")]
        [Required(ErrorMessage = "请输入帐户编号")]
        public string AccountCode { get; set; }

        /// <summary>
        /// 帐户名称
        /// </summary>
        [DisplayName("帐户名称")]
        [Column("AccountName", TypeName = "varchar(50)")]
        [Required(ErrorMessage = "请输入帐户名称")]
        public string AccountName { get; set; }

        /// <summary>
        /// 银行帐号
        /// </summary>
        [DisplayName("银行帐号")]
        [Column("BankAccount", TypeName = "varchar(19)")]
        public string BankAccount { get; set; }

        /// <summary>
        /// 会计科目
        /// </summary>
        [DisplayName("会计科目")]
        [Column("AccSubject", TypeName = "int")]
        public int AccSubject { get; set; }

        /// <summary>
        /// 期初金额
        /// </summary>
        [DisplayName("期初金额")]
        [Column("AccMoney", TypeName = "decimal(12, 2)")]
        public decimal AccMoney { get; set; }
    }
}
