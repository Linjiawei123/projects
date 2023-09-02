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
    /// 仓库
    /// </summary>
    public class BSStore
    {
        /// <summary>
        /// 数据id
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [DisplayName("数据id")]
        [Column("Id", TypeName = "int")]
        [Required(ErrorMessage = "请输入{0}的值")]
        public int Id { get; set; }
        /// <summary>
        /// 仓库编号
        /// </summary>
        [Key]
        [DisplayName("仓库编号")]
        [Column("StoreCode", TypeName = "varchar(10)")]
        [Required(ErrorMessage = "请输入仓库编号")]
        public string StoreCode { get; set; }

        /// <summary>
        /// 仓库名称
        /// </summary>
        [DisplayName("仓库名称")]
        [Column("StoreName", TypeName = "varchar(20)")]
        [Required(ErrorMessage = "请输入仓库名称")]
        public string StoreName { get; set; }

        /// <summary>
        /// 仓库面积
        /// </summary>
        [DisplayName("仓库面积")]
        [Column("Area", TypeName = "numeric(10,0)")]
        public decimal? Area { get; set; }

        /// <summary>
        /// 管理员姓名
        /// </summary>
        [DisplayName("管理员姓名")]
        [Column("EmployeeCode", TypeName = "varchar(10)")]
        public string EmployeeCode { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DisplayName("备注")]
        [Column("Remark", TypeName = "text")]
        public string Remark { get; set; }
    }

}
